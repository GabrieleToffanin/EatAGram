using Eatagram.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eatagram.Core.Data.EntityFramework.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> commentBuilder)
    {
        commentBuilder.HasOne(x => x.OfRecipe)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.RecipeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}