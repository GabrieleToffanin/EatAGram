using System.ComponentModel.DataAnnotations;

namespace Eatagram.Core.Entities.Token
{
    public class UserAuthentication
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
