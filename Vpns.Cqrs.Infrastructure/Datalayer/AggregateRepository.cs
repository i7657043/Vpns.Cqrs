﻿using Vpns.Cqrs.Domain.Models.Aggregates.Abstractions;
using Vpns.Cqrs.Domain.Models.Events.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Abstractions;
using Vpns.Cqrs.Infrastructure.Database.Fakes;

namespace Vpns.Cqrs.Infrastructure.Database
{
    public class AggregateRepository : IAggregateRepository
    {
        private readonly EventStore _eventStore;

        public AggregateRepository(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        //To test this we need to mock the event store with 2 events
        //Then mock a new aggregate using the create method on it and passing in some mocked events but with the same versions
        //Then call this method with the mocked aggregate and the mocked event store
        public void Persist<T>(T aggregate) where T : BaseAggregate
        {
            //If theres any events already in the event store with the same or a higher version number than we are trying to write, throw an exception
            long expectedVersion = aggregate.Events.First().Version;

            IDomainEvent? existingEvent = _eventStore.GetEvents(aggregate.AggregateId).FirstOrDefault(e => e.Version >= expectedVersion);
            if (existingEvent != null)
                throw new ArgumentOutOfRangeException($"aggregate version mismatch. version: {existingEvent.Version} already exists. cannot write version {expectedVersion}");            

            List<IDomainEvent> newEvents = aggregate.Events.ToList();
            
            _eventStore.AddEvents(newEvents);

            aggregate.ClearEvents();
        }

        public T Rehydrate<T>(Guid aggregateId) where T : BaseAggregate
        {
            List<IDomainEvent> events = _eventStore.GetEvents(aggregateId).ToList();

            T aggregate = BaseAggregate.Create<T>(events);

            return aggregate;
        }
    }
}
