using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Payment
{
    public class PaymentStatus : ValueObject<PaymentStatus>
    {
        public static string[] status { get; private set; } = { "paid", "on process", "decline" };
        public int number { get; private set; }
        public PaymentStatus(int status)
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
