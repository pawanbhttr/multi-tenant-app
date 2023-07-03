using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantApp.Application.Users.Commands.Authenticate;
using MultiTenantApp.Application.Users.Commands.Create;

namespace MultiTenantApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api")]
    [ApiController]
    public class SecurityController : BaseController
    {
        [ProducesResponseType(typeof(AuthenticateUserResponse), 200)]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthenticateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [ProducesResponseType(typeof(Guid), 200)]
        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
