using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Eatagram.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Data.EntityFramework.Repository
{
    /// <summary>
    /// Defines the Recipes repository
    /// </summary>
    public class RecipesRepository : IRecipeRepository
    {
        //Gets a field for the current dbContext
        private readonly ApplicationDbContext _dbContext;
        //Initializes actual services for the class to work
        public RecipesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates the recipe 
        /// </summary>
        /// <param name="recipe">current recipe that has to be added to the db</param>
        /// <returns>the current create recipe</returns>
        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            await _dbContext.AddAsync(recipe);
            await _dbContext.SaveChangesAsync();

            return await FindRecipeById(recipe.Id);
        }

        /// <summary>
        /// Deletes the current recipe from the database
        /// </summary>
        /// <param name="currentRecipe">current recipe to be removed</param>
        /// <returns>Return the recipe if deleted else null</returns>
        public async Task<Recipe> DeleteRecipe(Recipe currentRecipe)
        {
            var momentCopy = currentRecipe;

            _dbContext.Remove(currentRecipe);
            await _dbContext.SaveChangesAsync();

            return momentCopy;
        }

        /// <summary>
        /// Fetches all the recipes from the DataBase
        /// </summary>
        /// <returns>A collection of recipes</returns>
        public async Task<IEnumerable<Recipe>> FetchAllRecipes()
        {
            var items = await _dbContext.Recipes.Include(x => x.Ingredients)
                                                .Include(x => x.Owner)
                                                .OrderBy(x => x.Name)
                                                .ToListAsync();
            return items ?? Enumerable.Empty<Recipe>();
        }

        /// <summary>
        /// Finds the recipe by id
        /// </summary>
        /// <param name="id">Current supposed recipe id already present in DB </param>
        /// <returns>The fetched recipe</returns>
        public async Task<Recipe> FindRecipeById(int id)
        {
            return await _dbContext.Recipes.Include(x => x.Ingredients)
                                           .Include(x => x.Owner)
                                           .Where(x => x.Id == id)
                                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updated the recipe if found in db
        /// </summary>
        /// <param name="id">of the recipe already in DB</param>
        /// <param name="toUpdate">Data that will update the recipe</param>
        /// <returns>The updated recipe, with updated values</returns>
        public async Task<Recipe> UpdateRecipe(int id, Recipe toUpdate)
        {
            var current = await FindRecipeById(id);

            if(current == null) return null;

            await DeleteRecipe(current);
            var updated = await CreateRecipe(toUpdate);
            
            await _dbContext.SaveChangesAsync();

            return await FindRecipeById(updated.Id);
        }
    }
}
