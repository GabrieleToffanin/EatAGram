using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Framework.Testability.Authentication
{
    public class TestAuthenticationHandlerOptions : AuthenticationSchemeOptions
    {
        public string DefaultUserId { get; set; } = "UserId";
    }
}
