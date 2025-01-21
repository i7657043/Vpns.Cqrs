using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using Vpns.Cqrs.Domain.Models.Events;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Azure;
using Vpns.Cqrs.Infrastructure.Datalayer.Azure.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

        IEventStore eventStore = new AzEventStore(new AzOptions(
                TenantId: configuration["azure:tenantId"],
                ClientId: configuration["azure:clientId"],
                ClientSecret: configuration["azure:clientSecret"],
                CosmosUri: configuration["azure:cosmosUri"],
                CosmosDatabaseName: configuration["azure:cosmosDatabaseName"],
                CosmosContainerName: configuration["azure:cosmosContainerName"]));

        Guid id = Guid.NewGuid();

        await eventStore.AddEventsAsync(id, new List<IDomainEvent>()
            {
                new VpnCreatedEvent(id, 1, "vpnOne", "uk")
            });
    }
}