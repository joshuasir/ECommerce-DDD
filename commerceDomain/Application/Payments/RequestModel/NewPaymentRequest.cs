using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Payments.RequestModel
{
    public class NewPaymentRequest
    {
        public Guid orderId { get; set; }
        public Guid customerId { get; set; }
        public int total { get; set; }
    }
}
