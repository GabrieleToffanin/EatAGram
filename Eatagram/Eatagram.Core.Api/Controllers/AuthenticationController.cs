using Eatagram.Core.Api.Filter;
using Eatagram.Core.Api.Models.Contracts;
using Eatagram.Core.Api.Utils;
using Eatagram.Core.Entities.Authentication;
using Eatagram.Core.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eatagram.Core.Api.Controllers
{
    [DurationFilter]
    [ExceptionFilter]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationLogic _logic;

        public AuthenticationController(IAuthenticationLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        [Route("Authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(JwtTokenResponse))]
        public async Task<IActionResult> AuthenticateAsync([FromBody] JwtTokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Provided data is not correct");

            var response = await _logic.AuthenticateAsync(request);

            return Ok(response);
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(RegistrationResponse))]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest request)
        {
            var response = await _logic.RegisterAsync(request);

            if (response is null)
                return BadRequest("Provided data is not valid for registration");

            return Ok(response);
        }
    }
}
