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
    public class CommentController : ControllerBase
    {
        private readonly ICommentsLogic _commentsLogic;

        public CommentController(ICommentsLogic commentsLogic)
        {
            _commentsLogic = commentsLogic;
        }

        [HttpGet]
        [Authorize(Roles = "Memeber, Administrator")]
        [Route("GetRecipeComments/{id:int}")]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Comment>))]
        public async Task<IActionResult> GetRecipeComments([FromRoute] int id)
        {
            if (id is < 0)
                return BadRequest("Id must be greater than 0");

            var result = await _commentsLogic.FetchRecipeComments(id);

            if(result == null)
                return NotFound("Could not found comments for this recipe");

            return Ok(result.AsContracts(x => x.GetContract()));
        }

        [HttpPost]
        [Authorize(Roles = "Member, Administrator")]
        [Route("PostComment")]
        [ProducesResponseType(200, Type = typeof(CommentContract))]
        public async Task<IActionResult> PostComment([FromBody] CommentRequest comment)
        {
            if (!ModelState.IsValid)
                return BadRequest("Provided request has unvalid data");

            var current = comment.GetContract();
            current.User_Id = User.GetUserId();

            var result = await _commentsLogic.AddCommentOnRecipe(current);

            return Ok(result.GetContract());
        }
    }
}
