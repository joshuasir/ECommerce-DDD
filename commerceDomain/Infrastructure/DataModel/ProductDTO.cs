using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.DataModel
{
    public class ProductDTO
    {
        public Guid id { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }

        public Guid categoryId { get; set; }
        public char productSize { get; set; }
        public string productImage { get; set; }
        public int productPrice { get; set; }
        public char stsrc { get; set; }
    }
}
