using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantApp.Application.Common.Interfaces;
using MultiTenantApp.Domain.Entities;
using MultiTenantApp.Infrastructure.Common.Factories;

namespace MultiTenantApp.Infrastructure.Services
{
    public class DbMigrationService : IDbMigrationService
    {
        private readonly IServiceProvider _serviceProvider;
        public DbMigrationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task RunMigration(Tenant tenant)
        {
            var dbContextFactory = _serviceProvider.GetRequiredService<DbContextFactory>();
            var context = dbContextFactory.CreateDbContext(tenant);
            await context.Database.MigrateAsync();
        }
    }
}
