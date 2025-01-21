using Vpns.Cqrs.Domain.Models.Events.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Database.Fakes
{
    public class EventStore : IEventStore
    {
        private readonly Queue<IDomainEvent> _events = new();
        private readonly Dictionary<Guid, List<IDomainEvent>> _aggregateEventMap = new();

        public async Task AddEventsAsync(Guid aggregateId, IEnumerable<IDomainEvent> events)
        {
            foreach (var domainEvent in events)
            {
                _events.Enqueue(domainEvent);

                if (!_aggregateEventMap.ContainsKey(aggregateId))
                    _aggregateEventMap[aggregateId] = new List<IDomainEvent>();                

                _aggregateEventMap[aggregateId].Add(domainEvent);
            }
        }

        public IEnumerable<IDomainEvent> GetEvents(Guid aggregateId) => 
            _aggregateEventMap.TryGetValue(aggregateId, out var events) ? events : Enumerable.Empty<IDomainEvent>();
    }
}
