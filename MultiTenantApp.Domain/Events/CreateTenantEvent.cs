using MultiTenantApp.Domain.Common;
using MultiTenantApp.Domain.Entities;

namespace MultiTenantApp.Domain.Events
{
    public class CreateTenantEvent : DomainEvent
    {
        public CreateTenantEvent(Tenant tenant)
        {
            Tenant = tenant;
        }

        public Tenant Tenant { get; private set; }
    }
}
