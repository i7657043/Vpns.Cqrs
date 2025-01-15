using System;
using Vpns.Cqrs.Domain.Models.Aggregates;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Domain.Models.Events
{
    public record VpnCreatedEvent : BaseDomainEvent<Vpn>
    {
        public VpnCreatedEvent(Guid id, long version, string vpnTitle, string location) : base(id, version)
        {
            Title = vpnTitle;
            Location = location;
        }

        public string Title { get; init; }
        public string Location { get; init; }
    }

    public record VpnTitleUpdatedEvent : BaseDomainEvent<Vpn>
    {
        public VpnTitleUpdatedEvent(Guid id, long version, string vpnTitle) : base(id, version)
        {
            Title = vpnTitle;
        }

        public string Title { get; init; }
    }

    public record VpnLocationUpdatedEvent : BaseDomainEvent<Vpn>
    {
        public VpnLocationUpdatedEvent(Guid id, long version, string location) : base(id, version)
        {
            Location = location;
        }

        public string Location { get; init; }
    }
}
