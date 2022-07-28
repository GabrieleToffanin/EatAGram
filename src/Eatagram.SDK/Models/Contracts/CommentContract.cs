namespace Eatagram.SDK.Models.Contracts
{
    public class CommentContract
    {
        public int Id { get; set; }
        public string Content { get; init; }
        public int UpVotes { get; init; }
        public string User { get; set; }
        public string Recipe { get; init; }
    }
}
