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
        public async Task Vpn_Can_Be_Created_And_Updated()
        {
            //Arrange
            IAggregateRepository aggregateRepo = new AggregateRepository(new EventStore());

            //Act

            //CreateVpn Handler
            Vpn vpn = new Vpn().Create(Guid.NewGuid(), "vpn One", "uk");

            await aggregateRepo.PersistAsync(vpn);
            //

            //UpdateTitle Handler
            Vpn vpnFreshCopyFromRepo = aggregateRepo.Rehydrate<Vpn>(vpn.AggregateId);

            vpnFreshCopyFromRepo.UpdateTitle(vpnFreshCopyFromRepo.AggregateId, "another title");

            await aggregateRepo.PersistAsync(vpnFreshCopyFromRepo);
            //

            //UpdateLocation Handler
            Vpn vpnFreshCopyFromRepo2 = aggregateRepo.Rehydrate<Vpn>(vpn.AggregateId);

            vpnFreshCopyFromRepo2.UpdateLocation(vpn.AggregateId, "usa");

            await aggregateRepo.PersistAsync(vpnFreshCopyFromRepo2);
            //

            //Assert
            vpnFreshCopyFromRepo2.Title.Should().Be("another title");
            vpnFreshCopyFromRepo2.Location.Should().Be("usa");
        }

        [Test]
        public async Task Concurrent_Vpn_Update_Causes_Event_Version_Mismatch_And_Throws_Exception()
        {
            //Arrange
            Guid aggregateId = Guid.NewGuid();
            EventStore eventStore = new EventStore();
            await eventStore.AddEventsAsync(aggregateId, new List<IDomainEvent>()
            {
                new VpnCreatedEvent(aggregateId, 1, "vpnOne", "uk"),
                new VpnTitleUpdatedEvent(aggregateId, 2, "another title")
            });

            IAggregateRepository aggregateRepo = new AggregateRepository(eventStore);
            
            Vpn vpn = aggregateRepo.Rehydrate<Vpn>(aggregateId);

            //Simulate another event being added since rehydration of aggregate
            await eventStore.AddEventsAsync(aggregateId, new List<IDomainEvent>()
            {
                new VpnLocationUpdatedEvent(aggregateId, 3, "usa")
            });

            //Act
            vpn.UpdateTitle(vpn.AggregateId, "another title again");

            //Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => {
                await aggregateRepo.PersistAsync(vpn);
            });
        }
    }
}