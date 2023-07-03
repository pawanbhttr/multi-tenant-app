using MultiTenantApp.Application.Users.Commands.Authenticate;
using MultiTenantApp.Domain.Entities;
namespace MultiTenantApp.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateUserResponse> Authenticate(string username, string password);
        Task<bool> CreateAsync(User user, string password);
    }
}
