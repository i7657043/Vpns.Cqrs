using Vpns.Cqrs.Domain.Models.Events.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Database
{
    public class EventStore : IEventStore
    {
        private readonly Queue<IDomainEvent> _events = new Queue<IDomainEvent>();

        public void Create(IDomainEvent @event) =>
            _events.Enqueue(@event);
        
        public IEnumerable<IDomainEvent> GetEvents(Guid aggregateId) => 
            _events.Where(e => e.AggregateId == aggregateId).ToList();
    }
}
