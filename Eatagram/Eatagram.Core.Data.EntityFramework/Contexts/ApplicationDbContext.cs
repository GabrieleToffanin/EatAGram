﻿using Eatagram.Core.Entities;
using Eatagram.Core.Interfaces.Azure;
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
        private readonly IConnectionStringsProvider _connectionStringsProvider;

        //DbSet representing Recipes Table
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<Connection> Connections { get; set; } = null!;
        public DbSet<ConversationRoom> ConversationRooms { get; set; } = null!;

        public ApplicationDbContext(IConnectionStringsProvider connectionStringsProvider)
        {
            _connectionStringsProvider = connectionStringsProvider;
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStringsProvider.GetSqlConnectionString().GetAwaiter().GetResult());
            base.OnConfiguring(optionsBuilder);
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
