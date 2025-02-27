﻿using Vpns.Cqrs.Domain.Models.Aggregates;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Domain.Models.Events
{
    public record VpnCreatedEvent : BaseDomainEvent<Vpn>, IVpnDomainEvent
    {
        public VpnCreatedEvent(Guid vpnId, long version, string vpnTitle, string location) : base(version)
        {
            VpnId = vpnId;
            Title = vpnTitle;
            Location = location;
        }
        public Guid VpnId { get; init; }
        public string Title { get; init; }
        public string Location { get; init; }
    }
}
