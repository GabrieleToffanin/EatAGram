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

        private static IEnumerable<Recipe> GetSeedingRecipes()
        {
            yield return new Recipe
            {
                Name = "Cozze",
                Description = "Bone le cozze",
                Id = 1
            };
        }
    }
}