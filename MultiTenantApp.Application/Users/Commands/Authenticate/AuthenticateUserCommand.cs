using MediatR;
using MultiTenantApp.Application.Common.Interfaces;

namespace MultiTenantApp.Application.Users.Commands.Authenticate
{
    public class AuthenticateUserCommand : IRequest<AuthenticateUserResponse>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResponse>
    {
        private readonly IUserService _userService;

        public AuthenticateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AuthenticateUserResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var authenticateResult = await _userService.Authenticate(request.Username, request.Password);
            return authenticateResult;
        }
    }
}
