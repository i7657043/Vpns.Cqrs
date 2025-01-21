using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;
using Vpns.Cqrs.Infrastructure.Datalayer.Azure.Models;

namespace Vpns.Cqrs.Infrastructure.Database.Azure
{
    public class AzEventStore : IEventStore
    {
        private readonly Container _container;

        public AzEventStore(AzOptions azOptions) 
        {
            CosmosClient cosmosClient = new CosmosClient(
                azOptions.CosmosUri, 
                new DefaultAzureCredential(),
                new CosmosClientOptions() 
                { 
                    SerializerOptions = new CosmosSerializationOptions()
                    {
                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                    }
                });

            _container = cosmosClient.GetDatabase(azOptions.CosmosDatabaseName).GetContainer(azOptions.CosmosContainerName);
        }

        public async Task AddEventsAsync(Guid aggregateId, IEnumerable<IDomainEvent> events)
        {
            List<AzDomainEvent> cosmosEvents = events.Select(@event => 
                new AzDomainEvent(){ Data = @event, TimeStamp = DateTime.UtcNow,  Id = @event.EventId, AggregateId = aggregateId }).ToList();

            TransactionalBatch transactionalBatch = _container.CreateTransactionalBatch(new PartitionKey(aggregateId.ToString()));

            foreach (AzDomainEvent @event in cosmosEvents)
                transactionalBatch.CreateItem(@event);            

            TransactionalBatchResponse response = await transactionalBatch.ExecuteAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Transaction batch failed with status code {response.StatusCode} and error {response.ErrorMessage}");
        }

        public IEnumerable<IDomainEvent> GetEvents(Guid aggregateId)
        {
            //Need to add EventType as a String to the AzEvent
            //Then create String->Type mapping service that selects type based on EventType string arg from event

            throw new NotImplementedException();
        }
    }
}
