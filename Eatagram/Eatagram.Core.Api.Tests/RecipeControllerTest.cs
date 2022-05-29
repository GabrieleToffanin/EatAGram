using Eatagram.Core.Api.Controllers;
using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Api.Models.Requests;
using Eatagram.Core.Api.Tests.StabDb;
using Eatagram.Core.Entities;
using Eatagram.Core.Logic;
using Eatagram.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace Eatagram.Core.Api.Tests
{
    /// <summary>
    /// Class that has the task of testing RecipeController
    /// </summary>
    public class RecipeControllerTest : IClassFixture<ApiCoreWebApplicationFactory<RecipeController>>
    {
        private readonly ApiCoreWebApplicationFactory<RecipeController> _factory;
        private readonly HttpClient _httpClient;

        public RecipeControllerTest(ApiCoreWebApplicationFactory<RecipeController> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }



        /// <summary>
        /// Testing GET FetchAll endpoint method
        /// </summary>
        [Fact]
        public async Task ShouldReturnAllTheRecipes()
        {
            //*** Arrange
            //Creates a mockRepository with Moq Library
            var result = await _httpClient.GetAsync("api/Recipe/GetRecipes");

            var content = await result.Content.ReadAsStringAsync();

            var currentObjs = JsonConvert.DeserializeObject<IEnumerable<RecipeContract>>(content);
            var singleObj = currentObjs.FirstOrDefault();


            //*** Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(content != null);
            Assert.True(currentObjs.Any());
            Assert.True(singleObj.Name == "Cozze");
            Assert.True(singleObj.Description == "Bone le cozze");
        }

        [Fact]
        public async Task ShouldCreateARecipeWhenGoodDataProvided()
        {
            
            var recipeToAdd = new RecipeCreationRequest()
            {
                Name = "Tortellini in brodo",
                Description = "Bono"
            };


            var result = await _httpClient.PostAsJsonAsync("api/Recipe/CreateRecipe", recipeToAdd);

            var currentResult = await result.Content.ReadAsStringAsync();

            var content = JsonConvert.DeserializeObject<RecipeContract>(currentResult);


            Assert.True(content != null);
            Assert.True(content.Name == "Tortellini in brodo");
            Assert.True(content.Description == "Bono");

        }
    }
}