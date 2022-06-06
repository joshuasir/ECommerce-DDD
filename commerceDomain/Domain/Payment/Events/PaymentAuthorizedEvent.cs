using commerceDomain.Domain.Orders.Events;
using commerceDomain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Payment.Events
{
    public class PaymentAuthorizedEvent : DomainEvent
    {
        public Guid paymentId { get; private set; }

        public PaymentAuthorizedEvent(Guid paymentId)
        {
            this.paymentId = paymentId;
        }
    }
}
