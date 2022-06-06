using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Carts.RequestModel
{
    public class CartRequest
    {
        public Guid customerId { get; set; }
        public Guid id { get; set; }

    }
}
