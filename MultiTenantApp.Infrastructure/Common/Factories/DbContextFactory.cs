using Microsoft.EntityFrameworkCore;
using MultiTenantApp.Domain.Entities;
using MultiTenantApp.Infrastructure.Persistence;

namespace MultiTenantApp.Infrastructure.Common.Factories
{
    public class DbContextFactory
    {
        public AppDbContext CreateDbContext(Tenant tenant)
        {
            var options = new DbContextOptions<AppDbContext>();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>(options);
            return new AppDbContext(dbContextOptionsBuilder.Options, tenant);
        }
    }
}
