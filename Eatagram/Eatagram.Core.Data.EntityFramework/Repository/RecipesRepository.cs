﻿using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Eatagram.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Data.EntityFramework.Repository
{
    /// <summary>
    /// Defines the Recipes repository
    /// </summary>
    public class RecipesRepository : IRecipeRepository
    {
        //Gets a field for the current dbContext
        private readonly ApplicationDbContext _dbContext;
        //Initializes actual services for the class to work
        public RecipesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Fetches all the recipes from the DataBase
        /// </summary>
        /// <returns>A collection of recipes</returns>
        public async Task<IEnumerable<Recipe>> FetchAllRecipes()
        {
            return await _dbContext.Recipes.OrderBy(x => x.Name)
                                           .ToListAsync();
        }
    }
}
