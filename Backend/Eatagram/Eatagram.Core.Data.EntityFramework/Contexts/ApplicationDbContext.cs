﻿using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Chat;
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
        public DbSet<Comment> Comments { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
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

            modelBuilder.Entity<Comment>()
                        .HasOne(x => x.OfRecipe)
                        .WithMany(x => x.Comments)
                        .HasForeignKey(x => x.RecipeId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                        .HasOne(x => x.User)
                        .WithMany(x => x.Comments)
                        .HasForeignKey(x => x.User_Id)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ingredient>().HasIndex(x => x.Name).IsUnique();

            
        }
    }
}