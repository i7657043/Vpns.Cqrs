using Vpns.Cqrs.Domain.Models.Aggregates;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Domain.Models.Events
{
    public record VpnLocationUpdatedEvent : BaseDomainEvent<Vpn>, IVpnDomainEvent
    {
        public VpnLocationUpdatedEvent(Guid vpnId, long version, string location) : base(version)
        {
            VpnId = vpnId;
            Location = location;
        }

        public Guid VpnId { get; init; }
        public string Location { get; init; }
    }
}
