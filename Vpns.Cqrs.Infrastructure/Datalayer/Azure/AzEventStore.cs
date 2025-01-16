using Vpns.Cqrs.Domain.Models.Events.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Database.Azure
{
    internal class AzEventStore : IEventStore
    {
        public void AddEvents(IEnumerable<IDomainEvent> events)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDomainEvent> GetEvents(Guid aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}
