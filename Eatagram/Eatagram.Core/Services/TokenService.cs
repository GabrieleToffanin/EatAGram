using Eatagram.Core.Configuration;
using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Token;
using Eatagram.Core.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eatagram.Core.Services
{
    /// <summary>
    /// Service that allows AnonymousUsers to authenticate and register in the API
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtToken _token;
        private readonly HttpContext _httpContext;

        public TokenService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptions<JwtToken> token, IHttpContextAccessor httpContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _token = token.Value;
            _httpContext = httpContext.HttpContext;
        }

        /// <summary>
        /// Registration logic for enabling anonymous user to register, 
        /// 
        /// </summary>
        /// <param name="request">Actual request of registration coming from the endpoint</param>
        /// <returns>Returns true if the user does not exists in the database either false</returns>
        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            var response = new RegistrationResponse();
            var alreadyUser = await GetUserByEmail(request.Email);

            if (alreadyUser != null)
            {
                response.Message = $"An user is already registered with {request.Email}";
                response.Registered = false;
                return response;
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                Email = request.Email,
                LastName = request.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var registration = await _userManager.CreateAsync(user, request.Password);
            if (registration.Succeeded)
                await _userManager.AddToRoleAsync(user, ApplicationIdenityConstants.Roles.Member);

            response.Message = $"User registered with {request.Email}";
            response.Registered = true;

            return response;
        }

        /// <summary>
        /// Creates the token for the user present in the database,
        /// if the user is not registered returns null
        /// </summary>
        /// <param name="request">Request for the authentication</param>
        /// <param name="ipAddress">Current ipAddres where the request is coming from</param>
        /// <returns>returns an object with a token property in it, and some more general infos</returns>
        public async Task<JwtTokenResponse> Authenticate(JwtTokenRequest request, string ipAddress)
        {
            if (await IsValidUser(request.Username, request.Password))
            {
                ApplicationUser user = await GetUserByEmail(request.Username);

                if (user is not null)
                {
                    string role = (await _userManager.GetRolesAsync(user))[0];
                    string jwtToken = await GenerateJwtToken(user, role);

                    await _userManager.UpdateAsync(user);

                    return new JwtTokenResponse(user,
                        role,
                        jwtToken); //""//refreshToken.Token);
                }
            }

            return null;
        }
        /// <summary>
        /// Generates the token based on user claims, and HSA256 algorithm
        /// </summary>
        /// <param name="user">current user that's asking for the token generation</param>
        /// <param name="role">current user role, the token will adapt to rolekind</param>
        /// <returns>the token as string</returns>
        private async Task<string> GenerateJwtToken(ApplicationUser user, string role)
        {
            byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id),
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        /// <summary>
        /// Checks if the user is present in the db
        /// </summary>
        /// <param name="username">current application user username</param>
        /// <param name="password">current application user password</param>
        /// <returns>returns true if the user submitted good credentials, else false</returns>
        private async Task<bool> IsValidUser(string username, string password)
        {
            ApplicationUser user = await GetUserByEmail(username);

            if (user is null)
                return false;

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);

            return signInResult.Succeeded;
        }
        /// <summary>
        /// Checks for the user in the db byEmail
        /// </summary>
        /// <param name="email">Used for search of the application user</param>
        /// <returns>the foudn application user if any</returns>
        private async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
