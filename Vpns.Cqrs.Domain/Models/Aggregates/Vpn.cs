using Vpns.Cqrs.Domain.Models.Aggregates.Abstractions;
using Vpns.Cqrs.Domain.Models.Events;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Domain.Models.Aggregates
{
    public record Vpn : BaseAggregate
    {
        public string Title { get; private set; }
        public string Location { get; private set; }

        public Vpn() { }

        public Vpn(Guid aggregateId, string title, string location) : base(aggregateId) =>
            Apply(new VpnCreatedEvent(AggregateId, Version, title, location));

        public Vpn Create(Guid vpnId, string title, string location) => 
            new Vpn(vpnId, title, location);

        public void UpdateTitle(Guid vpnId, string title) =>
            Apply(new VpnTitleUpdatedEvent(AggregateId, Version, title));

        public void UpdateLocation(Guid vpnId, string location) =>
            Apply(new VpnLocationUpdatedEvent(AggregateId, Version, location));

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case VpnCreatedEvent e:
                    AggregateId = e.VpnId;
                    Title = e.Title;
                    Location = e.Title;
                    break;

                case VpnTitleUpdatedEvent e:
                    Title = e.Title;
                    break;

                case VpnLocationUpdatedEvent e:
                    Location = e.Location;
                    break;
            }
        }
    }
}
