using FluentAssertions;
using Vpns.Cqrs.Domain.Models.Aggregates;
using Vpns.Cqrs.Domain.Models.Events;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;
using Vpns.Cqrs.Infrastructure.Database;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Fakes;

namespace UnitTests
{
    public class VpnTests
    {
        [Test]
        public void Vpn_Can_Be_Created_And_Updated()
        {
            IAggregateRepository aggregateRepo = new AggregateRepository(new EventStore());

            //CreateVpn Handler
            Vpn vpn = new Vpn().Create(Guid.NewGuid(), "vpn One", "uk");

            aggregateRepo.Persist(vpn);
            //

            //UpdateTitle Handler
            Vpn vpnFreshCopyFromRepo = aggregateRepo.Rehydrate<Vpn>(vpn.AggregateId);

            vpnFreshCopyFromRepo.UpdateTitle(vpnFreshCopyFromRepo.AggregateId, "another title");

            aggregateRepo.Persist(vpnFreshCopyFromRepo);
            //

            //UpdateLocation Handler
            Vpn vpnFreshCopyFromRepo2 = aggregateRepo.Rehydrate<Vpn>(vpn.AggregateId);

            vpnFreshCopyFromRepo2.UpdateLocation(vpn.AggregateId, "usa");

            aggregateRepo.Persist(vpnFreshCopyFromRepo2);
            //

            vpnFreshCopyFromRepo2.Title.Should().Be("another title");
            vpnFreshCopyFromRepo2.Location.Should().Be("usa");
        }

        [Test]
        public void Vpn_Can_Be_Created_But_Not_Updated_Due_To_Event_Version_Mismatch()
        {
            Guid aggregateId = Guid.NewGuid();
            EventStore eventStore = new EventStore();
            eventStore.AddEvents(new List<IDomainEvent>()
            {
                new VpnCreatedEvent(aggregateId, 1, "vpnOne", "uk"),
                new VpnTitleUpdatedEvent(aggregateId, 2, "another title")
            });

            IAggregateRepository aggregateRepo = new AggregateRepository(eventStore);

            Vpn vpn = aggregateRepo.Rehydrate<Vpn>(aggregateId);

            Vpn vpnConcurrencyIssue = aggregateRepo.Rehydrate<Vpn>(aggregateId);

            vpn.UpdateTitle(vpn.AggregateId, "another title");

            vpnConcurrencyIssue.UpdateTitle(vpnConcurrencyIssue.AggregateId, "another title added late");

            aggregateRepo.Persist(vpn);

            Assert.Throws<ArgumentOutOfRangeException>(() => {
                aggregateRepo.Persist(vpnConcurrencyIssue);
            });
        }
    }
}