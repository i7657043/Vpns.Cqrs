using Vpns.Cqrs.Domain.Models.Aggregates.Abstractions;

namespace Vpns.Cqrs.Domain.Models.Events.Abstractions
{
    public abstract record BaseDomainEvent<T> : IDomainEvent
        where T : BaseAggregate
    {
        protected BaseDomainEvent(long version)
        {
            EventId = Guid.NewGuid();
            Version = version;
        }
        
        public Guid EventId { get; }
        public long Version { get; }
    }
}
