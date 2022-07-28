using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.SDK.Models.Authentication
{
    public class AuthenticationToken
    {
        public string DisplayName { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public string Token { get; init; }
        public string UserId { get; set; }
    }
}
