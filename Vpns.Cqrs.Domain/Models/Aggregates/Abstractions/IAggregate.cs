using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Domain.Models.Aggregates.Abstractions
{
    public interface IAggregate
    {
        Guid AggregateId { get; }
        long Version { get; }
        IReadOnlyCollection<IDomainEvent> Events { get; }
        void ClearEvents();
    }
}
