using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodMe.Core
{
    public abstract class DomainEvent<TAggregateId> : IEquatable<DomainEvent<TAggregateId>>
    {
        public TAggregateId AggregateId { get; }
        public Guid EventId { get; private set; }
        public long AggregateVersion { get; private set; }

        public DomainEvent(TAggregateId aggregateId, long aggregateVersion)
        {
            AggregateId = aggregateId;
            AggregateVersion = aggregateVersion;
            EventId = Guid.NewGuid();
        }

        public abstract bool Equals([AllowNull] DomainEvent<TAggregateId> other);
    }
}
