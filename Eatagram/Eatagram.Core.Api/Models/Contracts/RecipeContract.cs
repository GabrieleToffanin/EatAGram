using Eatagram.Core.Entities;

namespace Eatagram.Core.Api.Models.Contracts
{
    public class RecipeContract
    {

        public string Name { get; set; }

        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
