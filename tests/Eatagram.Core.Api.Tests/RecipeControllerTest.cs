using System.Net.Http.Json;
using Eatagram.Core.Api.Tests.Fixtures;
using Eatagram.SDK.Models.Contracts;
using Eatagram.SDK.Models.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Eatagram.Core.Api.Tests;

/// <summary>
///     Class that has the task of testing RecipeController
/// </summary>
public class RecipeControllerTest : IClassFixture<RecipeTestsFixture<Program>>
{
    private readonly HttpClient _client;

    public RecipeControllerTest(RecipeTestsFixture<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Theory]
    [InlineData("api/Recipe/GetRecipes")]
    [Trait("BDD", "GET")]
    public async Task ShouldFetchAllRecipesFromDbIfAny(string url)
    {
        //*** Arrange
        var response = await _client.GetAsync(url);

        //*** Act
        var content = await response.Content.ReadAsStringAsync();
        var itemsCollection = JsonConvert.DeserializeObject<IEnumerable<RecipeContract>>(content);

        //*** Assert
        Assert.NotNull(itemsCollection);
        Assert.IsAssignableFrom<IEnumerable<RecipeContract>>(itemsCollection);
        Assert.True(itemsCollection.Any());
    }

    [Theory]
    [InlineData("api/Recipe/CreateRecipe")]
    [Trait("BDD", "POST")]
    public async Task ShouldCreateRecipeWhenGoodDataProvided(string url)
    {
        //*** Arrange
        var fakeValue = "Cozze";
        var toCreate = new RecipeCreationRequest
        {
            Description = fakeValue,
            Name = fakeValue,
            Ingredients = new List<IngredientCreationRequest>
            {
                new()
                {
                    Name = fakeValue
                }
            }
        };
        //*** Act 
        var response = await _client.PostAsJsonAsync(url, toCreate);
        var content = await response.Content.ReadAsStringAsync();

        var @object = JsonConvert.DeserializeObject<RecipeContract>(content);

        //*** Assert
        Assert.NotNull(@object);
        Assert.Equal(@object.Name, toCreate.Name);
    }

    [Theory]
    [InlineData("api/Recipe/DeleteRecipe/")]
    [Trait("BDD", "DELETE")]
    public async Task ShouldDeleteRecipeIfIdFound(string url)
    {
        //*** Arrange
        const int id = 1;
        //*** Act
        var response = await _client.DeleteAsync($"{url}{id}");
        var content = await response.Content.ReadAsStringAsync();
        var @object = JsonConvert.DeserializeObject<RecipeContract>(content);
        //*** Assert

        Assert.NotNull(@object);
        Assert.IsAssignableFrom<RecipeContract>(@object);
    }

    [Theory]
    [InlineData("api/Recipe/UpdateRecipe/")]
    [Trait("BDD", "PUT")]
    public async Task SouldUpdateRecipeWhenGoodIdAndDataProvided(string url)
    {
        //*** Arrange
        const int id = 2;
        var request = new RecipeUpdateRequest
        {
            Name = "Cozze",
            Description = "Bone le cozze",
            Ingredients = new List<IngredientCreationRequest>
            {
                new()
                {
                    Name = "Cozze"
                }
            }
        };

        //*** Act
        var response = await _client.PutAsJsonAsync($"{url}{id}", request);
        var content = await response.Content.ReadAsStringAsync();
        var @object = JsonConvert.DeserializeObject<RecipeContract>(content);

        //*** Assert
        Assert.NotNull(@object);
        Assert.Equal(@object.Name, request.Name);
    }

    [Theory]
    [InlineData("api/Recipe/GetUserRecipes")]
    [Trait("BDD", "GET")]
    public async Task ShouldFetchOnlyRequestedUserRecipes(string url)
    {
        //*** Arrange
        var response = await _client.GetAsync(url);

        //*** Act
        var content = await response.Content.ReadAsStringAsync();

        var currentResponse = JsonConvert.DeserializeObject<IEnumerable<RecipeContract>>(content);
        var recipeContracts = currentResponse.ToList();
        var checkIfRightUser = recipeContracts.Where(x => x.User_Name == "Test user");

        //*** Assert
        Assert.NotNull(currentResponse);
        Assert.Equal(recipeContracts.Count, checkIfRightUser.Count());
    }
}