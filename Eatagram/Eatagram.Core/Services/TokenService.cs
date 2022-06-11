using Eatagram.Core.Configuration;
using Eatagram.Core.Entities;
using Eatagram.Core.Entities.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eatagram.Core.Services
{
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

        public async Task<UserRegistrationResponse> Register(UserRegistrationRequest request)
        {
            UserRegistrationResponse response = new UserRegistrationResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user != null)
            {
                response.Message = $"User already registered with {request.Email}";
                response.Success = false; 
                return response;
            }

            ApplicationUser userToCreate = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var result = await _userManager.CreateAsync(userToCreate, request.Password);
            if (result.Succeded)
                await _userManager.AddToRoleAsync(userToCreate, ApplicationIdenityConstants.Roles.Member);

            response.Message = $"User registerd with Email: {userToCreate.Email}";
            response.Success = true;

            return response;
        }

        public async Task<JwtTokenResponse> Authenticate(JwtTokenRequest request, string ipAddress)
        {
            if (await IsValidUser(request.Username, request.Password))
            {
                ApplicationUser user = await GetUserByEmail(request.Username);

                if (user is not null && user.IsEnabled)
                {
                    string role = (await _userManager.GetRolesAsync(user))[0];
                    string jwtToken = await GenerateJwtToken(user, role);

                    await _userManager.UpdateAsync(user);

                    return new JwtTokenResponse(user,
                                                role,
                                                jwtToken);
                }
            }

            return null;
        }

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

        private async Task<bool> IsValidUser(string username, string password)
        {
            ApplicationUser user = await GetUserByEmail(username);

            if (user is null)
                return false;

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);

            return signInResult.Succeeded;
        }

        private async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
