using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Api.Models.Requests;
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
        /// <returns>The list of entity Contracts</returns>
        public static IEnumerable<TResult> AsContracts<TEntity, TResult>(
            this IEnumerable<TEntity> entities,
            Func<TEntity, TResult> converter) where TEntity : class
                                              where TResult : class
        {
            foreach (var entity in entities)
                yield return converter(entity);
        }

        /// <summary>
        /// Determinate a contract for the Recipe
        /// </summary>
        /// <param name="recipe">Current recipe to be translated</param>
        /// <returns></returns>
        public static RecipeContract GetContract(this Recipe recipe)
            => new RecipeContract
            {
                Name = recipe.Name,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients.Select(x => x.Name).ToList(),
                User_Name = recipe.Owner.UserName
            };

        /// <summary>
        /// Converts request entity to his base Entity
        /// </summary>
        /// <param name="request">Actual request to be converted</param>
        /// <returns>The actual recipe achieved from the conversion</returns>
        public static Recipe GetContract(this RecipeCreationRequest request)
        {
            return new Recipe
            {
                Name = request.Name,
                Description = request.Description,
                Ingredients = request.Ingredients.AsContracts(x => x.GetContract()).ToList()
            };
        }
        /// <summary>
        /// Converts a RecipeDeletionRequest to his Recipe equivalent
        /// </summary>
        /// <param name="request">Actual request to convert</param>
        /// <returns>The parametrized Recipe</returns>
        public static Recipe GetContract(this RecipeUpdateRequest request)
        {
            return new Recipe
            {
                Name = request.Name,
                Description = request.Description,
                Ingredients = request.Ingredients.AsContracts(x => x.GetContract()).ToList()
            };
        }

        /// <summary>
        /// Request model for the creation of a recipe
        /// </summary>
        /// <param name="request">current request to be transposed to Ingredient Base</param>
        /// <returns>The converted in Ingredient</returns>
        public static Ingredient GetContract(this IngredientCreationRequest request)
        {
            return new Ingredient
            {
                Name = request.Name
            };
        }

        /// <summary>
        /// Tranforms an Ingredient in his IngredientContract equivalent
        /// </summary>
        /// <param name="ingredient">Current ingredient to be converted</param>
        /// <returns>The ingredient contract with same properties as the ingredient passed</returns>
        public static IngredientContract GetContract(this Ingredient ingredient)
        {
            return new IngredientContract()
            {
                Name = ingredient.Name,
                Recipes = ingredient.Recipes.Select(x => x.Name).ToList()
            };
        }

    }
}
