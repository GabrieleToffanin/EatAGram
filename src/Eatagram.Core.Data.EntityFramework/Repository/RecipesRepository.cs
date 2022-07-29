using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eatagram.Core.Data.EntityFramework.Repository
{
    /// <summary>
    /// Defines the Recipes repository, uses Eager Loading
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
        public async Task<Recipe?> CreateRecipe(Recipe recipe)
        {
            await _dbContext.Set<Recipe>().AddAsync(recipe);
            await _dbContext.SaveChangesAsync();

            return await FindRecipeById(recipe.Id);
        }

        /// <summary>
        /// Deletes the current recipe from the database
        /// </summary>
        /// <param name="currentRecipe">current recipe to be removed</param>
        /// <returns>Return the recipe if deleted else null</returns>
        public async Task<Recipe?> DeleteRecipe(Recipe? currentRecipe)
        {
            var momentCopy = currentRecipe;

#pragma warning disable CS8634
            _dbContext.Set<Recipe>().Remove(currentRecipe);
#pragma warning restore CS8634
            await _dbContext.SaveChangesAsync();

            return momentCopy;
        }

        /// <summary>
        /// Fetches all the recipes from the DataBase
        /// </summary>
        /// <returns>A collection of recipes</returns>
        public async Task<IEnumerable<Recipe>> FetchAllRecipes()
        {
            var items = await _dbContext.Set<Recipe>().Include(x => x.Ingredients)
                                                .Include(x => x.Comments)
                                                .OrderBy(x => x.Name)
                                                .AsNoTracking()
                                                .ToListAsync();

            return items ?? Enumerable.Empty<Recipe>();
        }

        /// <summary>
        /// Finds the recipe by id
        /// </summary>
        /// <param name="id">Current supposed recipe id already present in DB </param>
        /// <returns>The fetched recipe</returns>
        public async Task<Recipe?> FindRecipeById(int id)
        {
            return await _dbContext.Set<Recipe>().Include(x => x.Ingredients)
                                           .Include(x => x.Comments)
                                           .Where(x => x.Id == id)
                                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Recipe>> GetUserRecipe(Func<Recipe, bool> filter)
        {
            var userRecipes = _dbContext.Set<Recipe>().Include(x => x.Ingredients)
                .OrderBy(x => x.Name)
                .Where(recipe => filter(recipe))
                .AsNoTracking()
                .ToListAsync();

            return await userRecipes;
        }

        /// <summary>
        /// Updated the recipe if found in db
        /// </summary>
        /// <param name="id">of the recipe already in DB</param>
        /// <param name="toUpdate">Data that will update the recipe</param>
        /// <returns>The updated recipe, with updated values</returns>
        public async Task<Recipe?> UpdateRecipe(int id, Recipe toUpdate)
        {
            var current = await FindRecipeById(id);

            if (current == null) return null;

            current.Description = toUpdate.Description;
            current.Name = toUpdate.Name;
            current.Ingredients = toUpdate.Ingredients;

            await _dbContext.SaveChangesAsync();

            return await FindRecipeById(current.Id);
        }
    }
}
