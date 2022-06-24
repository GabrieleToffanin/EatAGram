using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Api.Models.Requests;
using Eatagram.Core.Api.Tests.Helper;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Eatagram.Core.Api.Tests
{
    /// <summary>
    /// Class that has the task of testing RecipeController
    /// </summary>
    public class RecipeControllerTest : IClassFixture<TestsBase<Program>>
    {
        private readonly TestsBase<Program> _factory;
        private readonly HttpClient _client;


        public RecipeControllerTest(TestsBase<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateDefaultClient();
        }
        [Fact]
        public async Task ShouldFetchAllRecipesFromDbIfAny()
        {

            //*** Arrange
            var respnse = await _client.GetAsync("api/Recipe/GetRecipes");

            //*** Act
            var content = await respnse.Content.ReadAsStringAsync();

            //*** Assert
            Assert.True(content != null);
        }
        [Fact]
        public async Task ShouldCreateRecipeWhenGoodDataProvided()
        {
            //*** Arrange
            var toCreate = new RecipeCreationRequest()
            {
                Description = "Cozze",
                Name = "Cozze",
                Ingredients = new List<IngredientCreationRequest>
                {
                    new IngredientCreationRequest
                    {
                        Name = "Cozze"
                    }
                }
            };
            //*** Act 
            var response = await _client.PostAsJsonAsync("api/Recipe/CreateRecipe", toCreate);
            var content = await response.Content.ReadAsStringAsync();

            var @object = JsonConvert.DeserializeObject<RecipeContract>(content);

            //*** Assert
            Assert.True(@object != null);
            Assert.True(@object.Name.Equals(toCreate.Name));

        }

        [Fact]
        public async Task ShouldDeleteRecipeIfIdFound()
        {
            //*** Arrange
            int id = 1;
            //*** Act
            var response = await _client.DeleteAsync($"api/Recipe/DeleteRecipe/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var @object = JsonConvert.DeserializeObject<RecipeContract>(content);
            //*** Assert

            Assert.True(@object != null);
            Assert.True(@object is RecipeContract);
        }
        [Fact]
        public async Task SouldUpdateRecipeWhenGoodIdAndDataProvided()
        {
            //*** Arrange
            int id = 2;
            var request = new RecipeUpdateRequest()
            {
                Name = "Cozze",
                Description = "Bone le cozze",
                Ingredients = new List<IngredientCreationRequest>
                {
                    new IngredientCreationRequest
                    {
                        Name = "Cozze"
                    }
                }

            };

            //*** Act
            var response = await _client.PutAsJsonAsync($"api/Recipe/UpdateRecipe/{id}", request);
            var content = await response.Content.ReadAsStringAsync();
            var @object = JsonConvert.DeserializeObject<RecipeContract>(content);

            //*** Assert
            Assert.True(@object != null);
            Assert.True(@object.Name.Equals(request.Name));

        }

        [Fact]
        public async Task ShouldFetchOnlyRequestedUserRecipes()
        {
            var response = await _client.GetAsync("api/Recipe/GetUserRecipes");
            var content = await response.Content.ReadAsStringAsync();

            var currentReponse = JsonConvert.DeserializeObject<IEnumerable<RecipeContract>>(content);
            var checkIfRightUser = currentReponse.Where(x => x.User_Name == "GT@outlook.it");

            Assert.True(currentReponse != null);
            Assert.True(currentReponse.Count() == checkIfRightUser.Count());
        }

    }
}