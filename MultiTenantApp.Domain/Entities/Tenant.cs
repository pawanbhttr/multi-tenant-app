using MultiTenantApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiTenantApp.Domain.Entities
{
    public class Tenant : IHasDomainEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
