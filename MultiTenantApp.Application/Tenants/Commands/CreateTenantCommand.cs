using MediatR;
using MultiTenantApp.Domain.Entities;
using MultiTenantApp.Domain.Events;
using MultiTenantApp.TenantHost.Persistence;

namespace MultiTenantApp.Application.Tenants.Commands
{
    public class CreateTenantCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Guid>
    {
        private readonly TenantDbContext _tenantDbContext;
        public CreateTenantCommandHandler(TenantDbContext tenantDbContext)
        {
            _tenantDbContext = tenantDbContext;
        }

        public async Task<Guid> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = new Tenant()
            {
                Name = request.Name
            };

            tenant.DomainEvents.Add(new CreateTenantEvent(tenant));

            if (_tenantDbContext.Tenants.Any(a => a.Name == tenant.Name))
                throw new Exception("Duplicate tenant!");

            _tenantDbContext.Add(tenant);
            await _tenantDbContext.SaveChangesAsync(cancellationToken);

            return tenant.Id;
        }
    }
}