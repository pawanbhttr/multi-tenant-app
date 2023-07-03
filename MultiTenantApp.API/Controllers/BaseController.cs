using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MultiTenantApp.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
