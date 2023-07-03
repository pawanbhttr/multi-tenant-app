namespace MultiTenantApp.Application.Users.Commands.Authenticate
{
    public class AuthenticateUserResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime TokenExpirationUTC { get; set; } = DateTime.Now;
    }
}
