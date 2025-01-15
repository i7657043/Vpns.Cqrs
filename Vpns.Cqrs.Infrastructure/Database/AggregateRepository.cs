using Vpns.Cqrs.Domain.Models.Aggregates.Abstractions;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Database
{
    public class AggregateRepository : IAggregateRepository
    {
        private readonly IEventStore _eventStore;

        public AggregateRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Persist<T>(T aggregate) where T : BaseAggregate
        {
            IDomainEvent latestEvent = aggregate.Events.First();
            long expectedVersion = latestEvent.AggregateVersion;
            if (aggregate.Version != expectedVersion + 1)
                throw new ArgumentOutOfRangeException($"aggregate version mismatch. Expected version: {expectedVersion + 1}, got {aggregate.Version}");

            List<IDomainEvent> previousAggregateEvents = 
                _eventStore.GetEvents(aggregate.AggregateId).Where(e => e.AggregateVersion >= expectedVersion).ToList();

            List<IDomainEvent> newEvents = aggregate.Events.ToList();

            foreach (IDomainEvent @event in newEvents)
                _eventStore.Create(@event);

            aggregate.ClearEvents();
        }

        public T Rehydrate<T>(Guid aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}
