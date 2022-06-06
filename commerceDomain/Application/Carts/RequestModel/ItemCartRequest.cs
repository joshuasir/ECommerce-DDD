using commerceDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Carts.RequestModel
{
    public class ItemCartRequest
    {
        public Guid id { get; set; }
        public Guid customerId { get; set; }
        public int quantity;
        public int price;
        public Product product;
    }
}
