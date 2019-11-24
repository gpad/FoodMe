using System.Collections.Generic;
using System.Linq;

namespace FoodMe.Core
{
    public class Aggregate<TAggregateId>
    {
        public const long NewAggregateVersion = -1;
        private long version = NewAggregateVersion;
        public long NextAggregateVersion => version + 1;

        private readonly List<DomainEvent<TAggregateId>> uncommittedEvents = new List<DomainEvent<TAggregateId>>();

        public TAggregateId Id { get; protected set; }

        protected void Emit(DomainEvent<TAggregateId> @event)
        {
            this.uncommittedEvents.Add(@event);
            Apply(@event);
        }

        protected void Apply(DomainEvent<TAggregateId> @event)
        {
            ((dynamic)this).When((dynamic)@event);
            this.version = @event.AggregateVersion;
        }
        protected void Apply(IEnumerable<DomainEvent<TAggregateId>> events)
        {
            events.ToList().ForEach(e => Apply(e));
        }

        public IEnumerable<DomainEvent<TAggregateId>> GetUncommittedEvents()
        {
            return this.uncommittedEvents;
        }

        public void ClearUncommittedEvents()
        {
            this.uncommittedEvents.Clear();
        }
    }
}
