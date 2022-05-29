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