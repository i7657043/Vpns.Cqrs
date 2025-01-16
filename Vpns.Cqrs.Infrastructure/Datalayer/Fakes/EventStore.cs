using Vpns.Cqrs.Domain.Models.Events.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Database.Fakes
{
    public class EventStore : IEventStore
    {
        private readonly Queue<IDomainEvent> _events = new Queue<IDomainEvent>();

        public void AddEvents(IEnumerable<IDomainEvent> events) =>
            events.ToList().ForEach(_events.Enqueue);

        public IEnumerable<IDomainEvent> GetEvents(Guid aggregateId) =>
            _events.Where(e => e.AggregateId == aggregateId).ToList();
    }
}
