namespace Eatagram.Core.Api.Models.Requests
{
    public class CommentRequest
    {
        public string Content { get; set; }
        public int RecipeId { get; set; }
    }
}
