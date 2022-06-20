using commerceDomain.Application.Payments.RequestModel;
using commerceDomain.Domain.Customers;
using commerceDomain.Domain.Orders;
using commerceDomain.Domain.Payment;
using commerceDomain.Domain.Payment.Events;
using commerceDomain.Domain.Payment.Services;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Payments
{
    public class MakePayment
    {
        private IPaymentRepository _paymentRepository;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private CommerceDatabaseContext _unitOfWork;

        public MakePayment(IPaymentRepository paymentRepository,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        CommerceDatabaseContext unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public Guid Create(NewPaymentRequest command)
        {
            new MailInvoicer(
                _customerRepository.FindBy(command.customerId).email, 
                "Transaction Invoice", 
                _orderRepository.FindBy(command.orderId)).SendEmail();

            var payment = Payment.CreateNew(command.orderId, command.customerId, command.total);

            _paymentRepository.Add(payment);
            _unitOfWork.SaveChanges();

            var paymentStream = _paymentRepository.FindByIdStream(payment.id);
            if (paymentStream.customerId == null)
            {
                _paymentRepository.CreateStream(payment);
            }
            paymentStream.Changes.Add(new PaymentCreatedEvent(payment.id));
            _paymentRepository.UpdateStream(paymentStream);

            return payment.id;
        }
    }
}
