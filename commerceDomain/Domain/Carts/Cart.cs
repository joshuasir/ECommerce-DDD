using commerceDomain.Domain.Carts.Events;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Carts
{
    public class Cart : EventSourcedAggregate<Guid>
    {

        public Guid customerId { get; set; }
        public List<CartItem> items { get; set; }

        public int calcTotalPrice()
        {
            return items.Select(a => a.quantity * a.currentPrice.value).Sum();
        }




        private Cart(Guid id) : base(id)
        {
            this.id = id;
        }

        private Cart(Guid id,Guid customerId) : base(id)
        {
            this.id = id;
            this.customerId = customerId;
        }

        public static Cart CreateNew(Guid customerId)
        {
            return new Cart(Guid.NewGuid(), customerId);
        }
        public static Cart CreateNew(Guid id, Guid customerId)
        {
            return new Cart(id, customerId);
        }
        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
            Version = Version++;
        }

        private void When(CartCheckoutEvent @event)
        {
            this.id = @event.Id;
            this.customerId = @event.customerId;
            items.Clear();
        }

        private void When(AddItemEvent @event)
        {
            items.Add(new CartItem(@event.quantity, @event.price, @event.product));
        }

        private void When(RemoveItemEvent @event)
        {
            items = items.Where(a => a.productDesc.id != @event.id).ToList();
        }

        private void When(UpdQtyEvent @event)
        {
            items.ForEach(a => {
                if (a.productDesc.id == @event.id)
                {
                    a.updQuantity(@event.qty);
                }
            });
        }
    }
}
