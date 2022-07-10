namespace Eatagram.Core.Entities.Token
{
    public class JwtTokenResponse
    {

        public string Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
