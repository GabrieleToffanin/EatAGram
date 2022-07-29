using Eatagram.Core.Api.Filter;
using Eatagram.Core.Api.Utils;
using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Comments;
using Eatagram.SDK.Models.Contracts;
using Eatagram.SDK.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eatagram.Core.Api.Controllers
{
    [DurationFilter]
    [ExceptionFilter]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentsLogic _commentsLogic;

        public CommentController(ICommentsLogic commentsLogic)
        {
            _commentsLogic = commentsLogic;
        }

        [HttpGet]
        [Authorize]
        [Route("GetRecipeComments/{id:int}")]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Comment>))]
        public async Task<IActionResult> GetRecipeComments([FromRoute] int id)
        {
            if (id < 0)
                return BadRequest("Id must be greater than 0");

            var result = await _commentsLogic.FetchRecipeComments(id);

            if (result is null)
                return NotFound("Could not found comments for this recipe");

            return Ok(result.AsContracts(recipe => recipe.GetContract()));
        }

        [HttpPost]
        [Authorize]
        [Route("PostComment")]
        [ProducesResponseType(200, Type = typeof(CommentContract))]
        public async Task<IActionResult> PostComment([FromBody] CommentRequest comment)
        {
            if (!ModelState.IsValid)
                return BadRequest("Provided request has not valid data");

            var current = comment.GetContract();

            current.OwnerName = User.GetUserId();

            var result = await _commentsLogic.AddCommentOnRecipe(current);

            return Ok(result.GetContract());
        }

        [HttpPost]
        [Authorize]
        [Route("UpvoteComment")]
        [ProducesResponseType(200, Type= typeof(CommentContract))]
        public async Task<IActionResult> UpVoteComment([FromBody]CommentUpvoteRequest commentUpvote)
        {
            if (commentUpvote.CommentId < 0)
                return BadRequest("Id must be greater than 0");

            Comment? result = await _commentsLogic.UpVoteCommentByIdAsync(commentUpvote.CommentId);


            if (result is null)
                return NotFound($"No comment with {commentUpvote.CommentId} id found");

            return Ok(result.GetContract());
        }
    }
}
