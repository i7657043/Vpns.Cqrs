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

        public static T Create<T>() where T : BaseAggregate
        {
            T responseObj = (T)Activator.CreateInstance(typeof(T))!;

            //We are going to replay the events onto the responseObj here (Aggregare.Apply)
            //then clear the events and return the object
            //This gets called from the agg_repo when rehydrating an aggregate, which is used in the command handler methods like updateTitle


            return responseObj;
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
