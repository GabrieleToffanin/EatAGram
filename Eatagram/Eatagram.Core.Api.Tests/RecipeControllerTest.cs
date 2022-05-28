using Eatagram.Core.Api.Controllers;
using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Entities;
using Eatagram.Core.Logic;
using Eatagram.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Eatagram.Core.Api.Tests
{
    /// <summary>
    /// Class that has the task of testing RecipeController
    /// </summary>
    public class RecipeControllerTest
    {
        /// <summary>
        /// Testing GET FetchAll endpoint method
        /// </summary>
        [Fact]
        public async Task ShouldReturnAllTheRecipes()
        {
            //*** Arrange
            //Creates a mockRepository with Moq Library
            var mockRepository = new Mock<IRecipeRepository>();
            //Setups a new db stub with default items in item
            mockRepository
                .Setup(r => r.FetchAllRecipes())
                .ReturnsAsync(new List<Recipe> { new Recipe { Id = 5, Description = "Piatto buonissimo", Name = "Pasta alla matriciana"} });

            //Initialize recipe brain logic with the mock repository
            IRecipeBrainLogic logic = new RecipeBrainLogic(mockRepository.Object);
            //Initializes a new controller with the business layer
            RecipeController controller = new RecipeController(logic);

            //*** Act
            var availableRecipes = await controller.GetRecipes();
            OkObjectResult result = availableRecipes as OkObjectResult;

            IEnumerable<RecipeContract> contentEnum = result.Value as IEnumerable<RecipeContract>;
            IList<RecipeContract> content = contentEnum.ToList();


            //*** Assert
            Assert.True(result != null);
            Assert.True(content != null);
            Assert.True(content.Any());
            Assert.True(content[0].Name == "Pasta alla matriciana");
            Assert.True(content[0].Description == "Piatto buonissimo");
        }
    }
}