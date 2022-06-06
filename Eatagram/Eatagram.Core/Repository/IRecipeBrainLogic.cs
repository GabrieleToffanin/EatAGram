using Eatagram.Core.Entities;

namespace Eatagram.Core.Repository
{
    /// <summary>
    /// This interface has the task of describing
    /// the actual possible operation to act
    /// on a Recipes Db
    /// </summary>
    public interface IRecipeBrainLogic
    {
        //Gets all the recipes
        Task<IEnumerable<Recipe>> GetAllRecipes();
        //Creates a new Recipe if goes well returns the latter else null
        Task<Recipe> CreateRecipe(Recipe currentRecipe);
        //Deletes the wanted Recipe from the db
        Task<Recipe> DeleteRecipe(int id);
        Task<Recipe> UpdateRecipe(int id, Recipe toUpdate);
    }
}
