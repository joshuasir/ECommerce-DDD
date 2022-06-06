using commerceDomain.Domain.Orders.Events;
using commerceDomain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Carts.Events
{
    public class CartCheckoutEvent : DomainEvent
    {
        public Guid customerId { get; private set; }
        public Guid cartId { get; private set; }

        public CartCheckoutEvent(Guid customerid,Guid cartId)
        {
            this.customerId = customerId;
            this.cartId = cartId;
        }
    }
}
