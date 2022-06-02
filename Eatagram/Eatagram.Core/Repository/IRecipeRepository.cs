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
        Task<Recipe> CreateRecipe(Recipe recipe);
        Task<Recipe> FindRecipeById(int id);
        Task<Recipe> DeleteRecipe(Recipe currentRecipe);
        Task<Recipe> UpdateRecipe(int id, Recipe toUpdate);
    }
}