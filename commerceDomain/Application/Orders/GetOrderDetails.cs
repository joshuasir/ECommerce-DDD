using commerceDomain.Domain;
using commerceDomain.Domain.Orders;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Orders
{
    public class GetOrderDetails
    {
        private IOrderRepository _orderRepository;

        public GetOrderDetails(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order GetLatest(Guid id)
        {
            return _orderRepository.FindBy(id);
        }

        public Order GetLatestStream(Guid id)
        {

            return _orderRepository.FindByIdStream(id);

        }
    }
}
