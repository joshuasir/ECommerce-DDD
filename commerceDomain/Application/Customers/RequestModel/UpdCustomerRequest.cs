using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Customers.RequestModel
{
    public class UpdCustomerRequest
    {
        public string name { get; private set; }
        public string email { get; private set; }
        public string street { get; private set; }
        public string postCode { get; private set; }
        public string city { get; private set; }
    }
}
