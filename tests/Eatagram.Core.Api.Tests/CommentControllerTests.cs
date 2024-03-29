﻿using Eatagram.Core.Api.Tests.Fixtures;
using Eatagram.Core.Api.Tests.Scenario.Comment;
using Eatagram.SDK.Models.Contracts;
using Eatagram.SDK.Models.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Json;

namespace Eatagram.Core.Api.Tests
{
    [DisplayName("CommentTests")]
    public class CommentControllerTests : IClassFixture<CommentTestsFixture<Program>>
    {
        private readonly HttpClient _client;
        public CommentControllerTests(CommentTestsFixture<Program> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Theory]
        [InlineData("api/Comment/GetRecipeComments/1")]
        [Trait("BDD", "GET")]
        public async Task ShouldFetchAllCommentsWhenGoingOnRouteWithAnExistingId(string url)
        {
            //*** Arrange
            //*** Act
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var itemsCollection = JsonConvert.DeserializeObject<IEnumerable<CommentContract>>(content);

            //*** Assert

            Assert.NotNull(itemsCollection);
            Assert.IsAssignableFrom<IEnumerable<CommentContract>>(itemsCollection);
            Assert.True(itemsCollection.Count() >= 0);

        }

        [Theory]
        [ClassData(typeof(BaseCommentScenario))]
        [Trait("BDD", "POST")]
        public async Task ShouldCreateCommentOnExistingRecipePostIfGoodDataProvided(
            string url,
            string content,
            int recipeId)
        {
            //*** Arrange
            var request = new CommentRequest
            {
                Content = content,
                RecipeId = recipeId,
                
            };

            //*** Act
            var response = await _client.PostAsJsonAsync(url, request);
            var retrievedContent = await response.Content.ReadAsStringAsync();
            var current = JsonConvert.DeserializeObject<CommentContract>(retrievedContent);

            //*** Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(current);
            Assert.IsAssignableFrom<CommentContract>(current);
            Assert.Equal(current.Content, content);
            Assert.NotEqual(current.Recipe, string.Empty);
        }

        [Theory]
        [InlineData("api/Comment/UpvoteComment")]
        [Trait("BDD", "POST")]
        public async Task ShouldUpvotePostIfIdFound(string url)
        {
            //*** Arrange
            var request = new CommentUpvoteRequest
            {
                CommentId = 1
            };

            //*** Act
            var response = await _client.PostAsJsonAsync(url, request);
            var retrieveContent = await response.Content.ReadAsStringAsync();
            var current = JsonConvert.DeserializeObject<CommentContract>(retrieveContent);

            //*** Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(current);
            Assert.Equal(2, current.UpVotes);
            Assert.IsType<CommentContract>(current);

        }
    }
}
