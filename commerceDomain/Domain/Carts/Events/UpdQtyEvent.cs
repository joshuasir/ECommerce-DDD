using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Carts.Events
{
    public class UpdQtyEvent : DomainEvent
    {
        public Guid id;
        public int qty;
        public UpdQtyEvent(Guid id, int qty)
        {
            this.id = id;
            this.qty = qty;
        }
    }
}
