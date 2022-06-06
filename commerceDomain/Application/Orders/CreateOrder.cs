using commerceDomain.Application.Orders.RequestModel;
using commerceDomain.Domain;
using commerceDomain.Domain.Orders;
using commerceDomain.Domain.Orders.Events;
using commerceDomain.Domain.Orders.Services;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Orders
{
    public class CreateOrder
    {
        private IOrderRepository _orderRepository;
        private CommerceDatabaseContext _unitOfWork;

        public CreateOrder(IOrderRepository orderRepository,
        CommerceDatabaseContext unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        public Guid Create(NewOrderRequest command)
        {
            var orderId = Guid.NewGuid();
            var order = Order.CreateNew(command.customerId, command.items, command.totals);
            _orderRepository.Add(order);
            _unitOfWork.SaveChanges();

            var orderStream = _orderRepository.FindByIdStream(order.id);
            if (orderStream.customerId == null)
            {
                _orderRepository.CreateStream(orderStream);
            }

            orderStream.Changes.Add(new OrderPlacedEvent(order.customerId, order.id));
            _orderRepository.UpdateStream(orderStream);

            return orderId;
        }

        public Currency GetShippingCost(IEnumerable<int> weightBand, int boxWeightInKg)
        {
           
            return new ShippingCostCalculator(weightBand, boxWeightInKg).CalculateCostToShip();
        }
    }
}
