namespace Vpns.Cqrs.Domain.Models.Events.Abstractions
{
    public interface IVpnDomainEvent
    {
        Guid VpnId { get; }
    }
}
