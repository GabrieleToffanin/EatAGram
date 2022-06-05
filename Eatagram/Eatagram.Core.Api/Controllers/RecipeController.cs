using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Api.Models.Requests;
using Eatagram.Core.Api.Utils;
using Eatagram.Core.Entities;
using Eatagram.Core.Repository;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Member")]
        [Route("GetRecipes")]
        [ProducesResponseType(200,Type = typeof(Recipe))]
        public async Task<IActionResult> GetRecipes() //Status code 200 Ok status code 400 BadRequest status 500 internal server error 
        {
            var recipes = await _recipeLogic.GetAllRecipes();

            return Ok(recipes.AsContracts(x => x.GetContract()));
        }

        [HttpPost]
        [Authorize(Roles = "Member")]
        [Route("CreateRecipe")]
        [ProducesResponseType(200, Type = typeof(RecipeContract))]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreationRequest recipeToAdd)
        {
            var currentRecipe = recipeToAdd.GetContract();

            currentRecipe.User_Id = User.GetUserId();

            var result = await _recipeLogic.CreateRecipe(currentRecipe);

            if (result == null)
                return BadRequest("Bad data for creation provided");

            return Ok(result.GetContract());
        }

        [HttpDelete]
        [Authorize(Roles = "Member")]
        [Route("DeleteRecipe/{id:int}")]
        [ProducesResponseType(200, Type = typeof(RecipeContract))]
        public async Task<IActionResult> DeleteRecipe([FromRoute]int id)
        {
            
            var result = await _recipeLogic.DeleteRecipe(id);

            if (result == null)
                return BadRequest("Bad id or Recipe not found");

            return Ok(result.GetContract());
        }

        [HttpPut]
        [Authorize(Roles = "Member")]
        [Route("UpdateRecipe/{id:int}")]
        [ProducesResponseType(200, Type = typeof(RecipeContract))]
        public async Task<IActionResult> UpdateRecipe([FromRoute] int id, [FromBody]RecipeUpdateRequest recipeUpdateRequest)
        {
            var toUpdate = recipeUpdateRequest.GetContract();

            toUpdate.User_Id = User.GetUserId();

            Recipe result = await _recipeLogic.UpdateRecipe(id, toUpdate);

            if (result == null)
                return BadRequest("Can't update with provided data, or object not found");

            return Ok(result.GetContract());
        }
    }
}