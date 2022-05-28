using Eatagram.Core.Entities;
using Eatagram.Core.Repository;

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
    }
}