using Eatagram.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eatagram.Core.Data.EntityFramework.Configurations;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> ingredientBuilder)
    {
        ingredientBuilder.HasMany(ingredient => ingredient.Recipes)
            .WithMany(recipe => recipe.Ingredients);

        ingredientBuilder.HasIndex(ingredient => ingredient.Name)
            .IsUnique();
    }
}