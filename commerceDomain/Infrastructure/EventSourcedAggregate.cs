using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure
{
    public abstract class EventSourcedAggregate<TId> : Entity<TId>
    {
        public List<DomainEvent> Changes { get; private set; }
        public int Version { get; protected set; }

        public EventSourcedAggregate(TId id)
        {
            Changes = new List<DomainEvent>();
            base.id = id;
        }
        public abstract void Apply(DomainEvent changes);
    }
}
