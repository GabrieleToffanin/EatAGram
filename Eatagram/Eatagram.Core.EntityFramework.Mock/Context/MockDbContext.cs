using Eatagram.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.EntityFramework.Mock.Context
{
    public sealed class MockDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public MockDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasData(
                    new Recipe { Description = "Buona la pasta", Name = "Spaghetti", Id = 1 },
                    new Recipe { Description = "Bone le cozze", Name = "Cozze", Id = 2 }
                );
        }

    }
}
