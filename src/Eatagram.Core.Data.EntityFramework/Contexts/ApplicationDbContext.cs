using Eatagram.Core.Data.EntityFramework.Configurations;
using Eatagram.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eatagram.Core.Data.EntityFramework.Contexts;

/// <summary>
/// Defines the Db Context for the application Eatagram
/// </summary>
public class ApplicationDbContext : DbContext
{
    //DbSet representing Recipes Table
    public DbSet<Recipe> Recipes { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}