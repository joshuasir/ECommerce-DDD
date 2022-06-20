using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain
{
    public class Size : ValueObject<Size>
    {
        public char size{get; private set; }
        
        public Size(char size)
        {
            this.size = size;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object> { size };
        }
    }
}
