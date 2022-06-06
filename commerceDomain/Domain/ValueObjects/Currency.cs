using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain
{
    public class Currency : ValueObject<Currency>
    {
        public int value { get; set; }
        public string formatted_value { get; set; }

        public Currency(int value) {
            this.value = value;
            this.formatted_value = "Rp. " + value.ToString();
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object> { value };
        }
    }
}
