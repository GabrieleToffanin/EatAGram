using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Comments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var toDelete = await _dbContext.Comments.FindAsync(commentId);

            if (toDelete == null)
                throw new InvalidOperationException("Comment not found in the database");

            _dbContext.Comments.Remove(toDelete!);
            await _dbContext.SaveChangesAsync();

            return toDelete;
        }

        public async Task<IEnumerable<Comment>> FetchMostUpVotedRecipeComment(int recipeId)
        {
            return await _dbContext.Comments.Include(x => x.User)
                                            .Include(x => x.OfRecipe)
                                            .ThenInclude(x => x.Ingredients)
                                            .Where(x => x.RecipeId == recipeId)
                                            .OrderBy(x => x.UpVoted)
                                            .Take(5)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> FetchSpecificRecipeComments(int recipeId)
        {
            return await _dbContext.Comments.Include(x => x.User)
                                            .Include(x => x.OfRecipe)
                                            .Where(x => x.RecipeId == recipeId)
                                            .ToListAsync();
        }

        public async Task<Comment> PostRecipeComment(Comment comment)
        {
            if (comment == null)
                throw new InvalidDataException("Invalid data to be inserted provided");

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            return await FindCommentById(comment.Id);

        }

        public async Task<Comment> FindCommentById(int id)
        {

            var result = await _dbContext.Comments.Include(x => x.User)
                                                  .Include(x => x.OfRecipe)
                                                  .ThenInclude(x => x.Ingredients)
                                                  .Where(x => x.Id == id)
                                                  .FirstOrDefaultAsync();
            return result;
        }

        public async Task<Comment> UpVoteCommentByIdAsync(int commentId)
        {
            var commentFound = await _dbContext.Comments.Where(x => x.Id == commentId)
                                                        .FirstOrDefaultAsync();

            if (commentFound is null)
                return null;

            commentFound.UpVoted += 1;

            await _dbContext.SaveChangesAsync();

            return await FindCommentById(commentId);                  
        }
    }
}
