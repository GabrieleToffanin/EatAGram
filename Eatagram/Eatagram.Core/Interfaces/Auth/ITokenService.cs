using Eatagram.Core.Entities.Token;
using Eatagram.Core.Entities.User;

namespace Eatagram.Core.Interfaces.Auth
{
    /// <summary>
    /// Service that manages the logic for the authentication
    /// </summary>
    public interface ITokenService
    {
        
        Task<JwtTokenResponse> Authenticate(JwtTokenRequest request, string ipAddress);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
