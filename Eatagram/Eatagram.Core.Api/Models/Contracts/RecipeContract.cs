namespace Eatagram.Core.Api.Models.Contracts
{
    public class RecipeContract
    {

        public string Name { get; set; }

        public string Description { get; set; }
        public List<string> Ingredients { get; set; }

        public string User_Name { get; set; }
    }
}
