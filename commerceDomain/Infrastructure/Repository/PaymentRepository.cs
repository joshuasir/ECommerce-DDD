using commerceDomain.Domain.Payment;
using commerceDomain.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CommerceDatabaseContext _commerceContext;
        private readonly IEventStore _eventStore;

        public PaymentRepository(CommerceDatabaseContext commerceContext, IEventStore eventStore)
        {
            _commerceContext = commerceContext;
            _eventStore = eventStore;
        }

        public void Add(Payment pay)
        {
            var paySS = pay.GetPaymentSnapshot();

            var paymentDTO = new PaymentDTO()
            {
                id = paySS.id,
                customerId = paySS.customerId,
                orderId = paySS.orderId,
                status = paySS.status,
                total = paySS.totalPrice,
                paidAt = paySS.PaidAt,
                createdAt = paySS.CreatedAt
            };

            _commerceContext.Payments.Add(paymentDTO);
        }

        public Payment FindBy(Guid Id)
        {
            var paymentDTO = _commerceContext.Payments.Find(Id);

            if (paymentDTO == null)
                throw new ArgumentNullException(nameof(paymentDTO));

            return Payment.CreateFromPaymentSnapshot(
            new PaymentSnapshot(){
                id = paymentDTO.id,
                orderId = paymentDTO.orderId,
                customerId = paymentDTO.customerId,
                status = paymentDTO.status,
                CreatedAt = paymentDTO.createdAt,
                PaidAt = paymentDTO.paidAt,
                totalPrice = paymentDTO.total
            });

        }

        private string StreamNameFor(Guid id)
        {
            // stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(Payment).Name, id);
        }

        public List<Payment> ViewTransactions()
        {
            return _commerceContext.Payments.Select(a => Payment.CreateNew(a.id,a.orderId,a.customerId,a.total,a.createdAt,a.paidAt)).ToList();
        }

        public void CreateStream(Payment payment)
        {
            var streamName = StreamNameFor(payment.id);
            _eventStore.CreateNewStream(streamName, payment.Changes);
        }

        public void UpdateStream(Payment payment)
        {
            var streamName = StreamNameFor(payment.id);
            _eventStore.AppendEventsToStream(streamName, payment.Changes, null);
        }

        public Payment FindByIdStream(Guid id)
        {
            var streamName = StreamNameFor(id);
            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;
            var stream = _eventStore.GetStream(
            streamName, fromEventNumber, toEventNumber
            );
            var payment = Payment.CreateNew(id);
            foreach (var @event in stream)
            {
                payment.Apply(@event);
            }
            return payment;
        }


    }
}
