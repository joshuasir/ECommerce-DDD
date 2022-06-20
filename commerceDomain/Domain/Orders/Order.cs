using commerceDomain.Domain.Carts;
using commerceDomain.Domain.Orders;
using commerceDomain.Domain.Orders.Events;
using commerceDomain.Infrastructure;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain
{
    public class Order : EventSourcedAggregate<Guid>
    {
        public Order(Guid id, Guid customerId,List<CartItem> items,  Currency totalPrice, OrderStatus status) : base(id)
        {
            this.id = id;
            this.customerId = customerId;
            this.totalPrice = totalPrice;
            this.items = items;
            this.status = status;
        }

        public Order(Guid id) : base(id)
        {
            this.id = id;
        }

        public Guid customerId { get; private set; }
        public OrderStatus status { get; private set; }
        public List<CartItem> items { get; private set; }
        public Currency totalPrice { get; private set; }

        internal static Order CreateNew(Guid customerId, List<CartItem> items, int totalPrice)
        {
            return new Order(Guid.NewGuid(), customerId, items, new Currency(totalPrice), new OrderStatus(1));
        }

        internal static Order CreateNew(Guid id,Guid customerId, List<CartItem> items, int totalPrice, int status)
        {
            return new Order(id, customerId, items, new Currency(totalPrice), new OrderStatus(status));
        }

        public static Order PlaceOrder(Guid customerId, Cart itemDetails, int totalPrice)
        {
            if (customerId == null)
                throw new Exception("The customer Id is required.");

            if (!itemDetails.items.Any())
                throw new Exception("An order should have at least one product.");

            if (itemDetails.calcTotalPrice() <=0)
                throw new Exception("The currency is required.");

            var order = Order.CreateNew(customerId, itemDetails.items, totalPrice);
            return order;
        }

        public void CalculateTotalPrice(Cart itemDetails)
        {
            totalPrice = new Currency(itemDetails.calcTotalPrice()) /* - tax */;
        }

        public OrderSnapshot GetOrderSnapshot()
        {
            return new OrderSnapshot()
            {
                id = this.id,
                customerId = this.customerId,
                totalPrice = this.totalPrice,
                CreatedAt = DateTime.Now

            };
        }

        public static Order CreateFromOrderSnapshot(OrderSnapshot orderSnapshot)
        {
            return new Order(orderSnapshot.id)
            {
                customerId = orderSnapshot.customerId,
                items = orderSnapshot.items,
                totalPrice = orderSnapshot.totalPrice,
                status = new OrderStatus(orderSnapshot.status)
            };
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
            Version = Version++;
        }

        private void When(OrderPlacedEvent @event)
        {
            this.id = @event.orderId;
            this.customerId = @event.customerId;
        }
    }
}
