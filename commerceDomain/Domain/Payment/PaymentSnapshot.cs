using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Payment
{
    public class PaymentSnapshot
    {
        public Guid id { get; set; }
        public Guid orderId { get; set; }
        public Guid customerId { get; set; }
        public int totalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public int status { get; set; }
    }
}
