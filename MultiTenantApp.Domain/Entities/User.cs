using Microsoft.AspNetCore.Identity;

namespace MultiTenantApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
    }
}
