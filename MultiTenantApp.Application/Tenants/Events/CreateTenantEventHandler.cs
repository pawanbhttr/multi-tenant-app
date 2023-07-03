using MediatR;
using MultiTenantApp.Application.Common.Interfaces;
using MultiTenantApp.Application.Common.Models;
using MultiTenantApp.Domain.Events;

namespace MultiTenantApp.Application.Tenants.Events
{
    public class CreateTenantEventHandler : INotificationHandler<DomainEventNotification<CreateTenantEvent>>
    {
        private readonly IDbMigrationService _dbMigrationService;
        public CreateTenantEventHandler(IDbMigrationService dbMigrationService)
        {
            _dbMigrationService = dbMigrationService;
        }
        public async Task Handle(DomainEventNotification<CreateTenantEvent> notification, CancellationToken cancellationToken)
        {
            await _dbMigrationService.RunMigration(notification.DomainEvent.Tenant);
        }
    }
}
