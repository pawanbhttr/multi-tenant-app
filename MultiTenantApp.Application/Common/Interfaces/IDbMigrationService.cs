using MultiTenantApp.Domain.Entities;
namespace MultiTenantApp.Application.Common.Interfaces
{
    public interface IDbMigrationService
    {
        Task RunMigration(Tenant tenant);
    }
}
