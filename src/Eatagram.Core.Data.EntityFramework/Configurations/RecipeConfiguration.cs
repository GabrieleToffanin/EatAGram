using Eatagram.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eatagram.Core.Data.EntityFramework.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> recipeBuilder)
    {
        recipeBuilder.HasMany(recipe => recipe.Ingredients)
            .WithMany(ingredient => ingredient.Recipes);

        recipeBuilder.HasMany(recipe => recipe.Comments)
            .WithOne(comment => comment.OfRecipe)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}