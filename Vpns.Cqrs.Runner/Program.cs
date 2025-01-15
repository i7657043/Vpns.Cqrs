using Vpns.Cqrs.Domain.Models.Aggregates;
using Vpns.Cqrs.Infrastructure.Database;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;

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

        //CreateVpn Handler
        Vpn vpnFreshCopyFromRepo2 = aggregateRepo.Rehydrate<Vpn>(vpn.AggregateId);

        vpnFreshCopyFromRepo2.UpdateLocation(vpn.AggregateId, "usa");

        aggregateRepo.Persist(vpn);
        //

        //create agg_repo
        //method 1 WRITE - persist the event in the eventstoreservice (should be in handler e.g. CreateAccountHandler) after apply
        //method 1 READ - read from eventstoreservice, call the baseAgg Create method that applies all the events we just got
    }
}