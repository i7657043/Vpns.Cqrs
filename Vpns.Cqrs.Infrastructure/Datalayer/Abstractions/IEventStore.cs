using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Database.Abstractions
{
    public interface IEventStore
    {
        Task AddEventsAsync(Guid aggregateId, IEnumerable<IDomainEvent> events);
        IEnumerable<IDomainEvent> GetEvents(Guid aggregateId);
    }
}
