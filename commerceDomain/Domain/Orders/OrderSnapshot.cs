using commerceDomain.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Orders
{
    public class OrderSnapshot
    {
        public Guid id { get; set; }
        public Guid customerId { get; set; }
        public Currency totalPrice { get; set; }
        public int status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CartItem> items { get; set; }
    }
}
