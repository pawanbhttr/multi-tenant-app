using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MultiTenantApp.Application.Common.Interfaces;
using MultiTenantApp.Application.Users.Commands.Authenticate;
using MultiTenantApp.Domain.Entities;
using MultiTenantApp.Infrastructure.Common.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiTenantApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtConfiguration _jwtConfiguration;

        public UserService(UserManager<User> userManager, IOptions<JwtConfiguration> jwtOptions)
        {
            _userManager = userManager;
            _jwtConfiguration = jwtOptions.Value;
        }

        public async Task<bool> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return result.Succeeded;
            }
            throw new Exception(string.Join(",", result.Errors.ToList().Select(x => x.Description)));
        }

        public async Task<AuthenticateUserResponse> Authenticate(string username, string password)
        {
            var dbUser = username.Contains('@') ? await _userManager.FindByEmailAsync(username) : await _userManager.FindByNameAsync(username);

            if (dbUser != null)
            {
                var result = await _userManager.CheckPasswordAsync(dbUser, password);
                if (result)
                {
                    var authenticationResult = new AuthenticateUserResponse
                    {
                        Token = await GenerateJWT(dbUser),
                        TokenExpirationUTC = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpireTimeInMinutes),
                        FirstName = dbUser.FirstName,
                        LastName = dbUser.LastName,
                    };
                    return authenticationResult;
                }
            }

            throw new Exception("Invalid username or password.");
        }

        private Task<string> GenerateJWT(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var scKey = Encoding.UTF8.GetBytes(_jwtConfiguration.Key);
            var signingSymmetricSecurityKey = new SymmetricSecurityKey(scKey);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtConfiguration.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpireTimeInMinutes),
                SigningCredentials = new SigningCredentials(signingSymmetricSecurityKey, SecurityAlgorithms.HmacSha512),
                Issuer = _jwtConfiguration.Issuer
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
