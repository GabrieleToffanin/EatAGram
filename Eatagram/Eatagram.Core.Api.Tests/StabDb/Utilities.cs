using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;

namespace Eatagram.Core.Api.Tests.StabDb
{
    internal static class Utilities
    {
        internal static void IntializeDbForTest(ApplicationDbContext db)
        {
            db.Recipes.AddRange(GetSeedingRecipes());
            db.SaveChanges();
        }

        internal static void ResetDbForTest(ApplicationDbContext db)
        {
            db.RemoveRange(GetSeedingRecipes());
            IntializeDbForTest(db);
        }

        private static IEnumerable<Recipe> GetSeedingRecipes()
        {
            Recipe[] recipes = new Recipe[] {
                new Recipe {
                Name = "Cozze",
                Description = "Bone le cozze",
                Id = 1
                } ,
                new Recipe {
                Name = "Spaghetti",
                Description = "Boni",
                Id = 2
                },
            };

            foreach (var item in recipes) yield return item;
        }
    }
}