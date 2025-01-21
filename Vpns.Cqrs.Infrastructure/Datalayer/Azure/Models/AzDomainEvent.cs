using Vpns.Cqrs.Domain.Models.Events.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Datalayer.Azure.Models
{
    public record AzDomainEvent
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public DateTime TimeStamp { get; set; }
        public IDomainEvent Data { get; set; }
    }
}
