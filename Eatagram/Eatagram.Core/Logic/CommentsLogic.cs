using Eatagram.Core.Entities;
using Eatagram.Core.Repository;
using Eatagram.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Logic
{
    public sealed class CommentsLogic : ICommentsLogic
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsLogic(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task<Comment> AddCommentOnRecipe(Comment comment)
        {
            if (ValidationUtils.Validate(comment).Count() > 0)
                return null;

            return await _commentsRepository.PostRecipeComment(comment);
        }

        public async Task<Comment> DeleteCommentOnRecipe(int commentId)
        {
            if (commentId < 0)
                return null;

            return await _commentsRepository.DeleteRecipeComment(commentId);
        }

        public async Task<IEnumerable<Comment>> FetchFirstFiveMostUpvotedComments(int recipeId)
        {
            if (recipeId < 0)
                return null;

            return await _commentsRepository.FetchMostUpVotedRecipeComment(recipeId);
        }

        public async Task<IEnumerable<Comment>> FetchRecipeComments(int recipeId)
        {
            if (recipeId < 0)
                return null;

            return await _commentsRepository.FetchMostUpVotedRecipeComment(recipeId);
        }

        public async Task<Comment> UpVoteCommentByIdAsync(int commentId)
        {
            return await _commentsRepository.UpVoteCommentByIdAsync(commentId);
        }
    }
}
