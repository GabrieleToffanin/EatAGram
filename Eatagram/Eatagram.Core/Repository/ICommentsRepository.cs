using Eatagram.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Repository
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> FetchSpecificRecipeComments(int recipeId);
        Task<IEnumerable<Comment>> FetchMostUpVotedRecipeComment(int recipeId);

        Task<Comment> PostRecipeComment(Comment comment);
        Task<Comment> DeleteRecipeComment(int commentId);
        Task<Comment> UpVoteCommentByIdAsync(int commentId);
    }
}
