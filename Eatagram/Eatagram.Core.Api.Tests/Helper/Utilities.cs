using Eatagram.Core.Configuration;
using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Eatagram.Core.Api.Tests.Helper
{
    internal sealed class Utilities
    {
        internal static void InitIdentityDb(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!db.Recipes.Any())
                AddDefaultAuthenticatedUser(userManager, roleManager);
                db.Recipes.AddRange(GetRecipesSeeding());
                db.Comments.Add(SeedComments());
                db.SaveChanges();
            
        }


        private static Comment SeedComments()
        {
            return new Comment()
            {
                Content = "Zao zao",
                Id = 1,
                RecipeId = 3,
                UpVoted = 1,
                User_Id = "2"
            };
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
                },
                new Recipe
                {
                    Description = "Fighi",
                    Id = 3,
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient {
                            Name = "Fango",
                            Id = 3
                        }
                    },
                    Name = "Pasta",
                    User_Id = "2"
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

            var normalUser = new ApplicationUser
            {
                Id = "2",
                UserName = "Testing@test.testing",
                Email = "Testing@test.testing",
                EmailConfirmed = true,
                FirstName = "Gabri",
                LastName = "Normal",

            };

            await userManager.CreateAsync(adminUser, ApplicationIdenityConstants.DefaultPassword);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, ApplicationIdenityConstants.Roles.Member);
            await userManager.CreateAsync(normalUser, ApplicationIdenityConstants.DefaultPassword);
            adminUser = await userManager.FindByNameAsync(normalUser.UserName);
            await userManager.AddToRoleAsync(normalUser, ApplicationIdenityConstants.Roles.Member);
        }
    }
}