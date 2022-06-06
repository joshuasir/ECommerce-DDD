using commerceDomain.Domain.Payment.Events;
using commerceDomain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Payment
{
    public class Payment : EventSourcedAggregate<Guid>
    {
        public Guid orderId { get; set; }
        public Guid customerId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? paidAt { get; set; }
        public PaymentStatus status { get; private set; }
        public Currency total { get; set; }

        public Payment(Guid id,Guid orderId, Guid customerId, PaymentStatus status , Currency total, DateTime CreatedAt,DateTime? PaidAt) : base(id)
        {
            this.id = id;
            this.orderId = orderId;
            this.customerId = customerId;
            this.status = status;
            this.total = total;
            this.createdAt = CreatedAt;
            this.paidAt = paidAt;
        }

        public Payment(Guid id) : base(id)
        {
            this.id = id;
        }

        public PaymentSnapshot GetPaymentSnapshot()
        {
            return new PaymentSnapshot() { 
                id = this.id,
                orderId = this.orderId,
                customerId = this.customerId,
                totalPrice = this.total.value,
                CreatedAt = DateTime.Now

            };
        }


        public static Payment CreateFromPaymentSnapshot(PaymentSnapshot paySnapshot)
        {
            return new Payment(
                paySnapshot.id,
                paySnapshot.orderId,
                paySnapshot.customerId,
                new PaymentStatus(paySnapshot.status),
                new Currency(paySnapshot.totalPrice),
                paySnapshot.CreatedAt,
                paySnapshot.PaidAt
            );

        }

        public void setStatus(int status)
        {
            if (status < 0 || status >=3)
                throw new Exception("Invalid Status.");

            this.status = new PaymentStatus(status);
        }

        internal static Payment CreateNew(Guid orderId, Guid customerId, int total)
        {
            return new Payment(Guid.NewGuid(), orderId, customerId, new PaymentStatus(1),new Currency(total),DateTime.Now, null);
        }

        internal static Payment CreateNew(Guid id,Guid orderId, Guid customerId, int total, DateTime createdAt, DateTime? paidAt)
        {
            return new Payment(id, orderId, customerId, new PaymentStatus(1), new Currency(total), createdAt, paidAt);
        }

        internal static Payment CreateNew(Guid id)
        {
            return new Payment(id);
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
            Version = Version++;
        }

        private void When(PaymentAuthorizedEvent @event)
        {
            this.id = @event.paymentId;
            this.status = new PaymentStatus(0);
        }

        private void When(PaymentCreatedEvent @event)
        {
            this.id = @event.paymentId;
            this.status = new PaymentStatus(1);
        }
    }
}
