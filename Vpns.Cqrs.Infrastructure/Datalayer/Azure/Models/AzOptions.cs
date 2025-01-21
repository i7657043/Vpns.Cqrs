namespace Vpns.Cqrs.Infrastructure.Datalayer.Azure.Models
{
    public record AzOptions(string TenantId, string ClientId, string ClientSecret, string CosmosUri, string CosmosDatabaseName, string CosmosContainerName);
}
