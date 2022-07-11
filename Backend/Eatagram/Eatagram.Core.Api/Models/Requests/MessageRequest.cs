namespace Eatagram.Core.Api.Models.Requests
{
    public record struct MessageRequest
    {
        public string Message { get; set; }
        public string ToUser { get; set; }
        public string? GroupName { get; set; }

    }
}
