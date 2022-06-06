using Eatagram.Core.Entities.Token;
using Eatagram.Core.Entities.User;

namespace Eatagram.Core.Repository
{
    public interface IAuthenticationLogic
    {
        Task<JwtTokenResponse> AuthenticateAsync(JwtTokenRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
