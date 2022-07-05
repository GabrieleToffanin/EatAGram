using Eatagram.Core.Entities.Authentication;
using Eatagram.Core.Entities.Token;

namespace Eatagram.Core.Interfaces.Auth
{
    /// <summary>
    /// Service that manages the logic for the authentication
    /// </summary>
    public interface ITokenService
    {
        
        Task<JwtToken> Authenticate(UserAuthentication request);
        Task<string> RegisterAsync(UserRegistration request);
    }
}
