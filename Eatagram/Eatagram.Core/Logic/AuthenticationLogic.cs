using Eatagram.Core.Entities.Token;
using Eatagram.Core.Repository;
using Eatagram.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string ipAddress = _httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            JwtTokenResponse jwtTokenResponse = await _tokenService.Authenticate(request, ipAddress);

            if (jwtTokenResponse is null)
                return null;

            return jwtTokenResponse;
        }
    }
}
