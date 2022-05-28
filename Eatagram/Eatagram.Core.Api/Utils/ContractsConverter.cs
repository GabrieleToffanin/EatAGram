using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Entities;

namespace Eatagram.Core.Api.Utils
{
    /// <summary>
    /// Static class who contains methods for
    /// entities to contracts conversion
    /// </summary>
    public static class ContractsConverter
    {
        /// <summary>
        /// Method that converts a collection IEnumerable to 
        /// a set of contracts
        /// </summary>
        /// <typeparam name="TEntity">Current entity to convert</typeparam>
        /// <typeparam name="TResult">Result where the entity will be converted to</typeparam>
        /// <param name="entities">Extension for the IEnumerable</param>
        /// <param name="converter">Actual type converter</param>
        /// <returns></returns>
        public static IEnumerable<TResult> AsContracts<TEntity, TResult>(
            this IEnumerable<TEntity> entities, 
            Func<TEntity, TResult> converter) where TEntity : class
                                              where TResult : class
        {
            foreach(var entity in entities)
                yield return converter(entity);
        }

        /// <summary>
        /// Determinate a contract for the Recipe
        /// </summary>
        /// <param name="recipe">Current recipe to be translated</param>
        /// <returns></returns>
        public static RecipeContract GetContract(this Recipe recipe)
            =>  new RecipeContract
                {
                    Name = recipe.Name,
                    Description = recipe.Description
                };
        
    }
}
