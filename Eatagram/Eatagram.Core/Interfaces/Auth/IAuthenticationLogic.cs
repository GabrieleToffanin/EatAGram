using Eatagram.Core.Entities.Authentication;
using Eatagram.Core.Entities.Token;

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
        Task<JwtToken> AuthenticateAsync(UserAuthentication request);
        /// <summary>
        /// Registers and user into the ASP NET identity, providing <see cref="UserRegistration"/> 
        /// </summary>
        /// <param name="request">Asks for the data for asking the registration</param>
        /// <returns>Returns a registration Response <see cref="RegistrationResponse"/></returns>
        Task<string> RegisterAsync(UserRegistration request);
    }
}
