using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eatagram.Core.Data.EntityFramework.Repository
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Comment> DeleteRecipeComment(int commentId)
        {
            var toDelete = await _dbContext.Set<Comment>().FindAsync(commentId);

            if (toDelete == null) throw new InvalidOperationException("Comment not found in the database");

            _dbContext.Set<Comment>().Remove(toDelete!);
            await _dbContext.SaveChangesAsync();

            return toDelete;
        }

        public async Task<IReadOnlyCollection<Comment>> FetchMostUpVotedRecipeComment(int recipeId)
        {
            return await _dbContext.Set<Comment>()
                .Include(x => x.OfRecipe)
                .ThenInclude(x => x.Ingredients)
                .Where(x => x.RecipeId == recipeId)
                .OrderBy(x => x.UpVoted)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Comment>> FetchSpecificRecipeComments(int recipeId)
        {
            return await _dbContext.Set<Comment>()
                .Include(x => x.OfRecipe)
                .Where(x => x.RecipeId == recipeId)
                .ToListAsync();
        }

        public async Task<Comment?> PostRecipeComment(Comment comment)
        {
            if (comment == null) throw new InvalidDataException("Invalid data to be inserted provided");

            await _dbContext.Set<Comment>().AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            return await FindCommentById(comment.Id);
        }

        private async Task<Comment?> FindCommentById(int id)
        {
            var result = await _dbContext.Set<Comment>()
                .Include(x => x.OfRecipe)
                .ThenInclude(x => x.Ingredients)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Comment?> UpVoteCommentByIdAsync(int commentId)
        {
            var commentFound = await _dbContext.Set<Comment>().Where(x => x.Id == commentId).FirstOrDefaultAsync();

            if (commentFound is null) return null;

            commentFound.UpVoted += 1;

            await _dbContext.SaveChangesAsync();

            return await FindCommentById(commentId);
        }
    }
}