using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Carts.Events
{
    public class AddItemEvent : DomainEvent
    {
        public int quantity;
        public int price;
        public Product product;

        public AddItemEvent(int quantity, int price, Product product) {
            this.quantity = quantity;
            this.price = price;
            this.product = product;
        }
    }
}
