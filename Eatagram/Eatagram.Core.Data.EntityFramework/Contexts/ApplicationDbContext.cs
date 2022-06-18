using Eatagram.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eatagram.Core.Data.EntityFramework.Contexts
{
    /// <summary>
    /// Defines the Db Context for the application Eatagram
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //DbSet representing Recipes Table
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;

        //Uses the default dbContext options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbOptions) : base(dbOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.Ingredients)
                .WithMany(x => x.Recipes);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Recipes)
                .WithOne(x => x.Owner)
                .HasForeignKey(x => x.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingredient>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
