using Eatagram.Core.Data.EntityFramework.Contexts;
using Eatagram.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Mocks.Data
{
    public sealed class FakeDataSeed
    {
        public static void IntitDatabase(ApplicationDbContext db)
        {
            db.Recipes.AddRange(entities: GetRecipesSeeding());
            db.Comments.Add(entity: SeedComments());

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
                OwnerName = "Test user"
            };
        }
        private static IEnumerable<Recipe?> GetRecipesSeeding()
        {
            return new List<Recipe?>
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
                    OwnerName = "Test user"
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
                    OwnerName = "Test user"
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
                    OwnerName = "Test user"
                }
            };
        }
    }
}
