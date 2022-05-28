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
    }
}