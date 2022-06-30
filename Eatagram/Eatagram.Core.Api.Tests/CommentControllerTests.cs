﻿using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Api.Models.Requests;
using Eatagram.Core.Api.Tests.Fixtures;
using Eatagram.Core.Api.Tests.Fixtures.Common;
using Eatagram.Core.Api.Tests.Scenario.Comment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Api.Tests
{
    [DisplayName("CommentTests")]
    public class CommentControllerTests : IClassFixture<CommentTestsFixture<Program>>
    {
        private readonly TestsBase<Program> _factory;
        private readonly HttpClient _client;

        public CommentControllerTests(CommentTestsFixture<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateDefaultClient();
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
                RecipeId = recipeId
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
    }
}
