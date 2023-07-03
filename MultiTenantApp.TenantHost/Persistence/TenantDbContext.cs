using Microsoft.EntityFrameworkCore;
using MultiTenantApp.Domain.Common;
using MultiTenantApp.Domain.Entities;
using MultiTenantApp.TenantHost.Interfaces;
using System.Reflection;

namespace MultiTenantApp.TenantHost.Persistence
{
    public class TenantDbContext : DbContext
    {
        private readonly IDomainEventService _domainEventService;
        public TenantDbContext(DbContextOptions<TenantDbContext> options, IDomainEventService domainEventService) : base(options)
        {
            _domainEventService = domainEventService;
        }

        public DbSet<Tenant> Tenants { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvent();
            return result;
        }

        private async Task DispatchEvent()
        {
            while (true)
            {
                var currentDomainEvents = ChangeTracker.Entries<IHasDomainEvent>().Select(s => s.Entity.DomainEvents);
                if (currentDomainEvents != null)
                {
                    var domainEventEntity = currentDomainEvents
                                                     .SelectMany(m => m)
                                                     .Where(w => !w.IsPublished)
                                                     .FirstOrDefault();
                    if (domainEventEntity == null) break;

                    domainEventEntity.IsPublished = true;

                    await _domainEventService.Publish(domainEventEntity);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
