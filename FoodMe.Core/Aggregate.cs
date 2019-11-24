using System.Collections.Generic;

namespace FoodMe.Core
{
    public class Aggregate<TAggregateId>
    {
        public const long NewAggregateVersion = -1;
        private long version = NewAggregateVersion;

        private readonly List<DomainEvent<TAggregateId>> uncommittedEvents = new List<DomainEvent<TAggregateId>>();

        public TAggregateId Id { get; protected set;}

        protected void Emit(DomainEvent<TAggregateId> @event)
        {
            this.uncommittedEvents.Add(@event);
            ((dynamic)this).When((dynamic)@event);
        }

        public IEnumerable<DomainEvent<TAggregateId>> GetUncommittedEvents()
        {
            return this.uncommittedEvents;
        }

    }


}
