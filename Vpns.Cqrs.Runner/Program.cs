using Vpns.Cqrs.Domain.Models.Aggregates;
using Vpns.Cqrs.Infrastructure.Database;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Fakes;

internal class Program
{
    private static async Task Main(string[] args)
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
    }
}