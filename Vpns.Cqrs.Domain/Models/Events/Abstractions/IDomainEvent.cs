namespace Vpns.Cqrs.Domain.Models.Events.Abstractions
{
    public interface IDomainEvent
    {
        long Version { get; }
        Guid AggregateId { get; }
    }
}
