using Eatagram.SDK.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.SDK.Interfaces
{
    public interface IAuthenticationProvider
    {
        Task<AuthenticationToken> AuthenticateUser();
    }
}
