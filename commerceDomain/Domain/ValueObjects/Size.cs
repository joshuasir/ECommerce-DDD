using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain
{
    public class Size : ValueObject<Size>
    {
        public char size{get; set;}
        
        public Size(char size)
        {
            this.size = size;
        }
    }
}
