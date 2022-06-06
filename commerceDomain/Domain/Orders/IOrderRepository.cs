using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Orders
{

    public interface IOrderRepository
    {
        void Add(Order order);
        Order FindBy(Guid Id);
        List<Order> GetByCustomer(Guid id);
        Order FindByIdStream(Guid id);
        void UpdateStream(Order order);
        void CreateStream(Order order);
    }
    
}
