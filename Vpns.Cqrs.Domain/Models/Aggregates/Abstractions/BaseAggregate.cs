using System.Collections.Immutable;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Domain.Models.Aggregates.Abstractions
{
    public abstract record BaseAggregate : IAggregate
    {
        private readonly Queue<IDomainEvent> _events = new Queue<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> Events => _events.ToImmutableArray();

        public Guid AggregateId { get; protected set; }
        public long Version { get; private set; } = 1;

        public BaseAggregate() { }

        protected BaseAggregate(Guid aggregateId) => AggregateId = aggregateId;

        public static T Create<T>(IEnumerable<IDomainEvent> events) where T : BaseAggregate
        {
            T aggregate = (T)Activator.CreateInstance(typeof(T))!;

            foreach (IDomainEvent @event in events)
                aggregate.Apply(@event);

            aggregate.ClearEvents();

            return aggregate;
        }

        public void Apply(IDomainEvent @event)
        {
            _events.Enqueue(@event);

            When(@event);

            Version++;
        }

        protected abstract void When(IDomainEvent @event);

        public void ClearEvents() => _events.Clear();
    }
}
