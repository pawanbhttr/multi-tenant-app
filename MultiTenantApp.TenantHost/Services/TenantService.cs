using Microsoft.AspNetCore.Http;
using MultiTenantApp.Domain.Entities;
using MultiTenantApp.TenantHost.Interfaces;
using MultiTenantApp.TenantHost.Persistence;

namespace MultiTenantApp.TenantHost.Services
{
    public class TenantService : ITenantService
    {
        private readonly TenantDbContext _dbContext;
        private readonly HttpContext? _httpContext;
        private Tenant? _currentTenant;
        public TenantService(IHttpContextAccessor contextAccessor, TenantDbContext dbContext)
        {
            _httpContext = contextAccessor.HttpContext;
            _dbContext = dbContext;

            if (_httpContext != null)
            {
                if (_httpContext.Request.Headers.TryGetValue("tenantId", out var tenantId))
                {
                    SetTenant(tenantId);
                }
                else
                {
                    throw new Exception("Invalid Tenant!");
                }
            }
        }
        private void SetTenant(string tenantId)
        {
            _currentTenant = _dbContext.Tenants.FirstOrDefault(f => f.Id.ToString() == tenantId);
            if (_currentTenant == null) throw new Exception("Invalid Tenant!");
        }

        public Tenant? GetTenant()
        {
            return _currentTenant;
        }

    }
}
