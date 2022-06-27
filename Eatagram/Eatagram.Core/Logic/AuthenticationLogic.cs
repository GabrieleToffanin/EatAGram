using Eatagram.Core.Entities.Token;
using Eatagram.Core.Entities.User;
using Eatagram.Core.Interfaces.Auth;
using Eatagram.Core.Utils;
using Microsoft.AspNetCore.Http;

namespace Eatagram.Core.Logic
{
    public class AuthenticationLogic : IAuthenticationLogic
    {
        private readonly ITokenService _tokenService;
        private readonly HttpContext _httpContext;

        public AuthenticationLogic(ITokenService tokenService, IHttpContextAccessor httpContext)
        {
            _tokenService = tokenService;
            _httpContext = httpContext.HttpContext;
        }

        public async Task<JwtTokenResponse> AuthenticateAsync(JwtTokenRequest request)
        {
            string ipAddress = _httpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString() ?? string.Empty;

            JwtTokenResponse jwtTokenResponse = await _tokenService.Authenticate(request, ipAddress);

            if (jwtTokenResponse is null)
                return null;

            return jwtTokenResponse;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            if (ValidationUtils.Validate(request).Count() > 0)
                return null;

            RegistrationResponse response = await _tokenService.RegisterAsync(request);

            return response;

        }
    }
}
