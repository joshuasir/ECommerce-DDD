using commerceDomain.Domain;
using commerceDomain.Domain.Orders;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Orders
{
    public class GetOrdersList
    {
        private IOrderRepository _orderRepository;

        public GetOrdersList(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> GetByCustomer(Guid id)
        {
            return _orderRepository.GetByCustomer(id);
        }

        
    }
}
