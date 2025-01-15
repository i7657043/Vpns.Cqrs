namespace Vpns.Cqrs.Domain.Models.Events.Abstractions
{
    public interface IDomainEvent
    {
        long AggregateVersion { get; }
        Guid AggregateId { get; }
    }
}
