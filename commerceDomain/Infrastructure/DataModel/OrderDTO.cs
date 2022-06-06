using commerceDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.DataModel
{
    public class OrderDTO
    {
        public Guid id { get; set; }
        public Guid customerId { get; set; }
        public string status { get; set; }
        public int totalPrice { get; set; }
        
    }
}
