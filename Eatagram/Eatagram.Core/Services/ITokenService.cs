using Eatagram.Core.Entities.Token;
using Eatagram.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Services
{
    public interface ITokenService
    {
        Task<JwtTokenResponse> Authenticate(JwtTokenRequest request, string ipAddress);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
