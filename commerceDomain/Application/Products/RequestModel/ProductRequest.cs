using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Products.RequestModel
{
    public class ProductRequest
    {
       public string name { get; set; }
       public string des { get; set; }
       public Guid categoryId { get; set; }
       public char size { get; set; }
       public string url { get; set; }
       public int price { get; set; }
    }
}
