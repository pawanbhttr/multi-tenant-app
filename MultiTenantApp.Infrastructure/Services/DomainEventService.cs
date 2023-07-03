using MediatR;
using MultiTenantApp.Application.Common.Models;
using MultiTenantApp.Domain.Common;
using MultiTenantApp.TenantHost.Interfaces;

namespace MultiTenantApp.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly IMediator _mediator;
        public DomainEventService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private static INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return Activator.CreateInstance(typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent) as INotification;
        }
    }
}
