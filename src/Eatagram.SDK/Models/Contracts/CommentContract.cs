namespace Eatagram.SDK.Models.Contracts
{
    public class CommentContract
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UpVotes { get; set; }
        public string User { get; set; }
        public string Recipe { get; set; }
    }
}
