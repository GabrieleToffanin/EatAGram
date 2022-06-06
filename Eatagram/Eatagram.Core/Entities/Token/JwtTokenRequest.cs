using System.ComponentModel.DataAnnotations;

namespace Eatagram.Core.Entities.Token
{
    public class JwtTokenRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
