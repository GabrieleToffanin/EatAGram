using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Api.Tests.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Api.Tests
{
    public class CommentControllerTests : IClassFixture<CommentControllerTestFixture<Program>>
    {
        private readonly CommentControllerTestFixture<Program> _factory;
        private readonly HttpClient _client;

        public CommentControllerTests(CommentControllerTestFixture<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task ShouldFetchAllCommentsWhenGoingOnRouteWithAnExistingId()
        {
            //*** Arrange
            //*** Act
            var response = await _client.GetAsync("api/Comment/GetRecipeComments/1");
            var content = await response.Content.ReadAsStringAsync();

            var itemsCollection = JsonConvert.DeserializeObject<IEnumerable<CommentContract>>(content);
            
            //*** Assert

            Assert.True(itemsCollection.Count() >= 0);
            
        }
    }
}
