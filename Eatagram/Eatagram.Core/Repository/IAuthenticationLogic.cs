using Eatagram.Core.Entities.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Repository
{
    public interface IAuthenticationLogic
    {
        Task<JwtTokenResponse> AuthenticateAsync(JwtTokenRequest request);
    }
}
