using commerceDomain.Application.Payments.RequestModel;
using commerceDomain.Domain.Customers;
using commerceDomain.Domain.Orders;
using commerceDomain.Domain.Payment;
using commerceDomain.Domain.Payment.Events;
using commerceDomain.Domain.Payment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Payments
{
    public class AuthorizePaymentHandler
    {
        private IPaymentRepository _paymentRepository;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
 

        public AuthorizePaymentHandler(IPaymentRepository paymentRepository,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }
        public Payment AuthorizePayment(AuthorizePaymentRequest command)
        {
            var paymentStream = _paymentRepository.FindByIdStream(command.id);

            if (paymentStream.orderId == null || paymentStream.customerId == null)
                throw new Exception("Invalid Payment.");

            new MailInvoicer(
                _customerRepository.FindBy(paymentStream.customerId).email,
                "Transaction Successfull",
                _orderRepository.FindBy(paymentStream.orderId)).SendEmail();

            paymentStream.Changes.Add(new PaymentAuthorizedEvent(paymentStream.id));
            _paymentRepository.UpdateStream(paymentStream);

            return paymentStream;
        }
    }
}
