using Eatagram.Core.Entities;

namespace Eatagram.Core.Interfaces.Logic
{
    /// <summary>
    /// This interface has the task of describing
    /// the actual possible operation to act
    /// on a Recipes Db
    /// </summary>
    public interface IRecipeBrainLogic
    {
        /// <summary>
        /// Gets all the recipes from the DB
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> where <see cref="{T}"/> is <seealso cref="Recipe"/></returns>
        Task<IEnumerable<Recipe>> GetAllRecipes();
        /// <summary>
        /// Filters and return the collection of recipes for a single User
        /// </summary>
        /// <param name="userFilter"><see cref="Func{T, TResult}"/> where <see cref="{T}"/> is <see cref="Recipe"/> and <see cref="{TResult}"/> is <see cref="bool"/></param>
        /// <returns>An <see cref="IEnumerable{T}"/> where T is <see cref="Recipe"/></returns>
        Task<IEnumerable<Recipe>> GetUserRecipes(Func<Recipe, bool> userFilter);
        /// <summary>
        /// Creates a recipe and inserts into db
        /// </summary>
        /// <param name="currentRecipe">Recipe to be created</param>
        /// <returns>The created Recipe</returns>
        Task<Recipe> CreateRecipe(Recipe currentRecipe);
        /// <summary>
        /// Deletes the recipe from the db from the DB
        /// </summary>
        /// <param name="id">Id to be searched and will delete the recipe with this ID</param>
        /// <returns>The deleted recipe</returns>
        Task<Recipe> DeleteRecipe(int id);
        /// <summary>
        /// Updates the recipe with the new content
        /// </summary>
        /// <param name="id">Id for faster search trough Put method</param>
        /// <param name="toUpdate">Recipe with updated content</param>
        /// <returns>The updated recipe with updated values</returns>
        Task<Recipe> UpdateRecipe(int id, Recipe toUpdate);
    }
}
