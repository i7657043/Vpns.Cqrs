using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Database.Abstractions
{
    public interface IEventStore
    {
        void Create(IDomainEvent @event);
        IEnumerable<IDomainEvent> GetEvents(Guid aggregateId);
    }
}
