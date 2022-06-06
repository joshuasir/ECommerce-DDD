using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Carts.Events
{
    public class RemoveItemEvent : DomainEvent
    {
        public Guid id;

        public RemoveItemEvent(Guid id)
        {
            this.id = id;
        }
    }
}
