using commerceDomain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Orders.Events
{

    public class OrderPlacedEvent : DomainEvent
    {
        public Guid customerId { get; private set; }
        public Guid orderId { get; private set; }

        public OrderPlacedEvent(Guid customerId, Guid orderId)
        {
            this.customerId = customerId;
            this.orderId = orderId;

        }
    }
}