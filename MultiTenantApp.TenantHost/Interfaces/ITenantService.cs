using MultiTenantApp.Domain.Entities;

namespace MultiTenantApp.TenantHost.Interfaces
{
    public interface ITenantService
    {
        Tenant? GetTenant();
    }
}
