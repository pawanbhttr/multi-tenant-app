using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiTenantApp.Application.Common.Interfaces;
using MultiTenantApp.Domain.Entities;
using MultiTenantApp.TenantHost.Interfaces;

namespace MultiTenantApp.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, string>, IAppDbContext
    {
        private readonly Tenant? _tenant;
        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantService tenantService) : base(options) 
        {
            _tenant = tenantService.GetTenant();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, Tenant tenant) : base(options)
        {
            _tenant = tenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_tenant is not null)
                optionsBuilder.UseSqlServer($"Server=.;Database=Tenant_{_tenant.Name};Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
