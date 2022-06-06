using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.DataModel
{
    public class OrderDetailDTO
    {
        public Guid orderId { get; set; }
        public Guid orderDetailId { get; set; }
        public Guid productId { get; set; }
        public int quantity { get; set; }
        public int totalPrice { get; set; }
    }
}
