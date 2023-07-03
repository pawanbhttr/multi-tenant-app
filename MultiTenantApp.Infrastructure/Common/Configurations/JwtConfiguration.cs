namespace MultiTenantApp.Infrastructure.Common.Configurations
{
    public class JwtConfiguration
    {
        public const string SECTION_NAME = "JWT";
        public string Key { get; set; } = string.Empty;
        public int ExpireTimeInMinutes { get; set; }
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty; 
    }
}
