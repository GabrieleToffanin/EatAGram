using Eatagram.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Repository
{
    public interface ICommentsLogic
    {
        Task<IEnumerable<Comment>> FetchRecipeComments(int recipeId);
        Task<IEnumerable<Comment>> FetchFirstFiveMostUpvotedComments(int recipeId);

        Task<Comment> AddCommentOnRecipe(Comment comment);
        Task<Comment> DeleteCommentOnRecipe(int commentId);
    }
}
