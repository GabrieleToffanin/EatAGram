using Eatagram.Core.Entities;

namespace Eatagram.Core.Repository
{
    /// <summary>
    /// Interface representing the recipes repostiory
    /// possible methods that can be used
    /// </summary>
    public interface IRecipeRepository
    {
        //Gets the method for getting all the recipes 
        //from the DAL
        Task<IEnumerable<Recipe>> FetchAllRecipes();
        /// <summary>
        /// Creates a recipe from a RecipeCreationRequest
        /// </summary>
        /// <param name="recipe">Recipe to be created</param>
        /// <returns>The created recipe if data OK, else null</returns>
        Task<Recipe> CreateRecipe(Recipe recipe);
        /// <summary>
        /// Finds the recipe by id
        /// </summary>
        /// <param name="id">The id for finding the recipe in the db</param>
        /// <returns>The found recipe or null if not found</returns>
        Task<Recipe> FindRecipeById(int id);
        /// <summary>
        /// Deletes the recipe from the db
        /// </summary>
        /// <param name="currentRecipe">The recipe to be deleted</param>
        /// <returns>The deleted recipe, else null</returns>
        Task<Recipe> DeleteRecipe(Recipe currentRecipe);
        /// <summary>
        /// Updates a recipe by his id and the new data passed as RecipeUpdateRequest
        /// </summary>
        /// <param name="id">Id of the recipe for the update HTTPPut verb</param>
        /// <param name="toUpdate">Current data that will update the already present recipe</param>
        /// <returns>The recipe with updated values</returns>
        Task<Recipe> UpdateRecipe(int id, Recipe toUpdate);
    }
}