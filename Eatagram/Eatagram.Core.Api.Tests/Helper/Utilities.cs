using Eatagram.Core.Configuration;
using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Eatagram.Core.Api.Tests.Helper
{
    internal sealed class Utilities
    {
        private readonly UserManager<ApplicationUser> userManager;

        internal static void InitIdentityDb(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            AddDefaultAuthenticatedUser(userManager, roleManager);
            db.Recipes.AddRange(GetRecipesSeeding());
            db.SaveChanges();
        }

        private static Recipe[] GetRecipesSeeding()
        {
            return new Recipe[]
            {
                new Recipe
                {
                    Description = "Bona la pasta",
                    Id = 1,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient {
                            Name = "Fagiolini",
                            Id = 1,
                        }
                    },
                    Name = "Pasta",
                    User_Id = "1"
                },
                new Recipe
                {
                    Description = "Bona la pasta",
                    Id = 2,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient {
                            Name = "Cancaro",
                            Id = 2
                        }
                    },
                    Name = "Pasta",
                    User_Id = "1"
                }
            };
        }

        private static async Task AddDefaultAuthenticatedUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(ApplicationIdenityConstants.Roles.Administrator));
            await roleManager.CreateAsync(new IdentityRole(ApplicationIdenityConstants.Roles.Member));

            string adminUserName = "GT@outlook.it";

            var adminUser = new ApplicationUser
            {
                Id = "1",
                UserName = adminUserName,
                Email = adminUserName,
                EmailConfirmed = true,
                FirstName = "Gabriele",
                LastName = "Admin",
                
            };

            await userManager.CreateAsync(adminUser, ApplicationIdenityConstants.DefaultPassword);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, ApplicationIdenityConstants.Roles.Member);
        }
    }
}