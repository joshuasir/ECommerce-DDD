using commerceDomain.Domain;
using commerceDomain.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Orders.RequestModel
{
    public class NewOrderRequest
    {
        public Guid customerId { get; set; }
        public List<CartItem> items { get; set; }
        public int totals { get; set; }
    }
}
