using Eatagram.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Data.EntityFramework.Contexts
{
    /// <summary>
    /// Defines the Db Context for the application Eatagram
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //DbSet representing Recipes Table
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

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
        }


    }
}
