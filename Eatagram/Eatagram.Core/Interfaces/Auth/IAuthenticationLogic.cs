using Eatagram.Core.Entities.Token;
using Eatagram.Core.Entities.User;

namespace Eatagram.Core.Interfaces.Auth
{
    /// <summary>
    /// Provides method for authentication and registration
    /// </summary>
    public interface IAuthenticationLogic
    {
        /// <summary>
        /// Authenticates an already registered user providing a JWT token
        /// </summary>
        /// <param name="request">Asks for username and password</param>
        /// <returns>The JWT token or null if user not found</returns>
        Task<JwtTokenResponse> AuthenticateAsync(JwtTokenRequest request);
        /// <summary>
        /// Registers and user into the ASP NET identity, providing <see cref="RegistrationRequest"/> 
        /// </summary>
        /// <param name="request">Asks for the data for asking the registration</param>
        /// <returns>Returns a registration Response <see cref="RegistrationResponse"/></returns>
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
