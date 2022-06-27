using Eatagram.Core.Entities.Token;
using Eatagram.Core.Entities.User;

namespace Eatagram.Core.Interfaces.Auth
{
    public interface ITokenService
    {
        Task<JwtTokenResponse> Authenticate(JwtTokenRequest request, string ipAddress);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
