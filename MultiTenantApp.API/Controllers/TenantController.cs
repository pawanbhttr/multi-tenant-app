using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantApp.Application.Tenants.Commands;

namespace MultiTenantApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> CreateTenantAsync([FromBody] CreateTenantCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
