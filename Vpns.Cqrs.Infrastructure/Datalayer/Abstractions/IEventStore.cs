using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Database.Abstractions
{
    public interface IEventStore
    {
        void AddEvents(IEnumerable<IDomainEvent> events);
        IEnumerable<IDomainEvent> GetEvents(Guid aggregateId);
    }
}
