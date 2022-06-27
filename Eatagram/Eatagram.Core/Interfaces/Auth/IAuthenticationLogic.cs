using Eatagram.Core.Entities.Token;
using Eatagram.Core.Entities.User;

namespace Eatagram.Core.Interfaces.Auth
{
    public interface IAuthenticationLogic
    {
        Task<JwtTokenResponse> AuthenticateAsync(JwtTokenRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
