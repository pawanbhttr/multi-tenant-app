namespace MultiTenantApp.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }

    public class DomainEvent
    {
        public bool IsPublished { get; set; }
    }
}
