using Vpns.Cqrs.Domain.Models.Aggregates.Abstractions;

namespace Vpns.Cqrs.Domain.Models.Events.Abstractions
{
    public abstract record BaseDomainEvent<T> : IDomainEvent
        where T : BaseAggregate
    {
        protected BaseDomainEvent(Guid id, long version)
        {
            AggregateId = id;
            AggregateVersion = version;
        }

        public long AggregateVersion { get; }
        public Guid AggregateId { get; }
    }
}
