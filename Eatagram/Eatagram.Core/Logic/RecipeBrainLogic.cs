using Eatagram.Core.Entities;
using Eatagram.Core.Repository;
using Eatagram.Core.Utils;

namespace Eatagram.Core.Logic
{
    /// <summary>
    /// Logic for manipulating Recipe Entity
    /// </summary>
    public class RecipeBrainLogic : IRecipeBrainLogic
    {
        //Sets the readonly field for accessing the repository 
        private readonly IRecipeRepository _recipeRepository;
        //Initialize the services for the class
        public RecipeBrainLogic(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        /// <summary>
        /// Checks if entity is valid and asks the DAL to create the new Entity
        /// </summary>
        /// <param name="currentRecipe">Current asked for entity </param>
        /// <returns>Return the created recipe if good data else null</returns>
        public async Task<Recipe> CreateRecipe(Recipe currentRecipe)
        {
            //Performs the validation on the Entity
            if (ValidationUtils.Validate(currentRecipe).Count() > 0)
                return null;

            //Returns the entity cause of success validation
            return await _recipeRepository.CreateRecipe(currentRecipe);
        }
        /// <summary>
        /// Calls the DAL for deleting the recipe from the database
        /// </summary>
        /// <param name="id">Current id of the recipe to be deleted</param>
        /// <returns>If Recipe not found in db null, else the deleted recipe</returns>
        public async Task<Recipe> DeleteRecipe(int id)
        {
            Recipe currentRecipe = await _recipeRepository.FindRecipeById(id);

            if (currentRecipe == null)
                return null;

            return await _recipeRepository.DeleteRecipe(currentRecipe);

        }

        /// <summary>
        /// Gets all the recipes from the DAL
        /// </summary>
        /// <returns>A collection of Recipes if any</returns>
        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            //Gets the current available recipes from the DAL
            IEnumerable<Recipe> recipes = await _recipeRepository.FetchAllRecipes();

            //Returns the recipes if any, else an empty Enumerable of type Recipe
            return recipes ?? Enumerable.Empty<Recipe>();
        }

        /// <summary>
        /// Updates the recipe in the database
        /// </summary>
        /// <param name="id">current recipe id in the database</param>
        /// <param name="toUpdate">current data that the already present Recipe will be updated with</param>
        /// <returns>the updated recipe from the database</returns>
        public async Task<Recipe> UpdateRecipe(int id, Recipe toUpdate)
        {
            if (ValidationUtils.Validate(toUpdate).Count() > 0)
                return null;

            return await _recipeRepository.UpdateRecipe(id, toUpdate);
        }
    }
}