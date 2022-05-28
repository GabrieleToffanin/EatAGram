using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Api.Utils;
using Eatagram.Core.Entities;
using Eatagram.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Eatagram.Core.Api.Controllers
{
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeBrainLogic _recipeLogic;

        public RecipeController(IRecipeBrainLogic logic)
        {
            _recipeLogic = logic;
        }

        [HttpGet]
        [Route("GetRecipes")]
        [ProducesResponseType(200,Type = typeof(RecipeContract))]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _recipeLogic.GetAllRecipes();

            return Ok(recipes.AsContracts(x => x.GetContract()));
        }

    }
}