using MediatR;
using MultiTenantApp.Application.Common.Interfaces;
using MultiTenantApp.Domain.Entities;

namespace MultiTenantApp.Application.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<string>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                PhoneNo = request.PhoneNo
            };

            await _userService.CreateAsync(user, request.Password);
            return user.Id;
        }
    }
}
