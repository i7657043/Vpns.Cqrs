namespace Vpns.Cqrs.Domain.Models.Events.Abstractions
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        long Version { get; }
    }
}
