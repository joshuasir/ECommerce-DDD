using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.DataModel
{
    public class PaymentDTO
    {
        public Guid id { get; set; }
        public Guid orderId { get; set; }
        public Guid customerId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? paidAt { get; set; }
        public int status { get; set; }
        public int total { get; set; }
    }
}
