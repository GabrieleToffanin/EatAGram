using Eatagram.Core.Entities;

namespace Eatagram.Core.Interfaces.Repository
{
    public interface ICommentsRepository
    {
        Task<IReadOnlyCollection<Comment>> FetchSpecificRecipeComments(int recipeId);
        Task<IReadOnlyCollection<Comment>> FetchMostUpVotedRecipeComment(int recipeId);

        Task<Comment?> PostRecipeComment(Comment comment);
        Task<Comment> DeleteRecipeComment(int commentId);
        Task<Comment?> UpVoteCommentByIdAsync(int commentId);
    }
}
