using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Carts
{
    public class CartItem : ValueObject<CartItem>
    {
        public Product productDesc { get; set; }
        public int quantity { get; set; }
        public Currency currentPrice { get; set; }

        public void updQuantity(int qty)
        {
            this.quantity = qty;
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object> { productDesc, quantity, currentPrice };
        }

        public CartItem(int quantity, int price, Product product) {
            this.quantity = quantity;
            currentPrice = new Currency(price);
            productDesc = product;
        }
        
    }
}
