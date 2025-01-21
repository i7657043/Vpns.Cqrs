using Vpns.Cqrs.Domain.Models.Aggregates;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Domain.Models.Events
{
    public record VpnTitleUpdatedEvent : BaseDomainEvent<Vpn>, IVpnDomainEvent
    {
        public VpnTitleUpdatedEvent(Guid vpnId, long version, string vpnTitle) : base(version)
        {
            VpnId = vpnId;
            Title = vpnTitle;
        }

        public Guid VpnId { get; init; }
        public string Title { get; init; }
    }
}
