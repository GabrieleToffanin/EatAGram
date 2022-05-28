using Eatagram.Core.Entities;
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
    public class ApplicationDbContext : DbContext
    {
        //DbSet representing Recipes Table
        public DbSet<Recipe> Recipes { get; set; }

        //Uses the default dbContext options
        public ApplicationDbContext(DbContextOptions dbOptions) : base(dbOptions)
        {
        }
    }
}
