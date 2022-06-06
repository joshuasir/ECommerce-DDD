using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Orders
{
    public class OrderStatus : ValueObject<OrderStatus>
    {
        public static string[] status { get; private set; } = { "finished", "on process", "pending" };
        public int number { get; private set; }
        public OrderStatus(int status)
        {
            number = status;
        }

        public string getStatus()
        {
            return status[number];
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object> { number };
        }
    }
}
