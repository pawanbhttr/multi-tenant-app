using MultiTenantApp.Domain.Common;

namespace MultiTenantApp.TenantHost.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
