﻿using Vpns.Cqrs.Domain.Models.Aggregates.Abstractions;

namespace Vpns.Cqrs.Infrastructure.Database.Abstractions
{
    public interface IAggregateRepository
    {
        Task PersistAsync<T>(T aggregate) where T : BaseAggregate;
        T Rehydrate<T>(Guid aggregateId) where T : BaseAggregate;
    }
}
