using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.DataModel
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public char newsLetter { get; set; }
        public char stsrc { get; set; }
    }
}
