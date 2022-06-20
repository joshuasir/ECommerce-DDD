using commerceDomain.Domain;
using commerceDomain.Domain.Carts;
using commerceDomain.Domain.Orders;
using commerceDomain.Infrastructure.DataModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CommerceDatabaseContext _commerceContext;
        private readonly IEventStore _eventStore;

        public OrderRepository(CommerceDatabaseContext commerceContext, IEventStore eventStore)
        {
            _commerceContext = commerceContext;
            _eventStore = eventStore;
        }

        public void Add(Order order)
        {
            var orderSS = order.GetOrderSnapshot();

            var orderDTO = new OrderDTO() {
                id = orderSS.id,
                customerId = orderSS.customerId,
                totalPrice = orderSS.totalPrice.value,
                status = orderSS.status
            };

            _commerceContext.Orders.Add(orderDTO);

            orderSS.items.ForEach(a =>
            {
                _commerceContext.OrderDetails.Add(new OrderDetailDTO() { 
                    orderId = orderSS.id,
                    orderDetailId = Guid.NewGuid(),
                    quantity = a.quantity,
                    totalPrice = a.currentPrice.value,
                    productId = a.productDesc.id,
                    
                });

            });
        }

        public Order FindBy(Guid id)
        {
            var orderDTO = _commerceContext.Orders.Find(id);
            var orderDetailDTO = _commerceContext.OrderDetails.Where(a=>a.orderId==id).
                Join(_commerceContext.Products.ToList(),
                orderDetail => orderDetail.productId,
                product => product.id,
                (orderDetail,product) => new { 
                    orderDetail,
                    product
                })
                .Select(a=> new CartItem(
                    a.orderDetail.quantity,
                    a.orderDetail.totalPrice,
                    Product.CreateNew(
                        a.product.id,
                        a.product.productName,
                        a.product.productImage,
                        a.product.categoryId,
                        a.product.productSize,
                        a.product.productImage,
                        a.product.productPrice
                    )
                )).ToList();

            var oderSS = new OrderSnapshot()
            {
                id = orderDTO.id,
                customerId = orderDTO.customerId,
                totalPrice = new Currency(orderDTO.totalPrice),
                items = orderDetailDTO,
                status = orderDTO.status
            };

            return Order.CreateFromOrderSnapshot(oderSS);
        }

        public List<Order> GetByCustomer(Guid id)
        {
            var orderDTO = _commerceContext.Orders.
                Where(a => a.customerId == id).
                Select(a => Order.CreateFromOrderSnapshot(new OrderSnapshot()
                {
                    id = a.id,
                    customerId = a.customerId,
                    totalPrice = new Currency(a.totalPrice),
                    items = null,
                    status = a.status
                })).ToList();
 
            return orderDTO;
        }

        private string StreamNameFor(Guid id)
        {
            // stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(Order).Name, id);
        }

        public void CreateStream(Order order)
        {
            var streamName = StreamNameFor(order.id);
            _eventStore.CreateNewStream(streamName, order.Changes);
        }

        public void UpdateStream(Order order)
        {
            var streamName = StreamNameFor(order.id);
            _eventStore.AppendEventsToStream(streamName, order.Changes, null);
        }

        public Order FindByIdStream(Guid id)
        {
            var streamName = StreamNameFor(id);
            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;
            var stream = _eventStore.GetStream(
            streamName, fromEventNumber, toEventNumber
            );
            var order = new Order(id);
            foreach (var @event in stream)
            {
                order.Apply(@event);
            }
            return order;
        }
        /*
        public List<Order> GetByCustomer(Guid id)
        {
            var orderDTO = _commerceContext.Orders.Where(a=>a.customerId == id).ToList();
            var orderDetailDTO = orderDTO.
                GroupJoin(_commerceContext.OrderDetails.ToList(),
                header=>header.id,
                detail=>detail.orderId,
                (header,detail)=>new { 
                    header, 
                    detail = detail.ToList().
                Join(_commerceContext.Products.ToList(),
                orderDetail => orderDetail.productId,
                product => product.id,
                (orderDetail, product) => new {
                    orderDetail,
                    product
                })
                .Select(a => new CartItem(
                    a.orderDetail.quantity,
                    a.orderDetail.totalPrice,
                    Product.CreateNew(
                        a.product.id,
                        a.product.productName,
                        a.product.productImage,
                        a.product.categoryId,
                        a.product.productSize,
                        a.product.productImage,
                        a.product.productPrice
                    )
                )).ToList()}).
                Select(a=> Order.CreateFromOrderSnapshot(new OrderSnapshot()
                {
                    id = a.header.id,
                    customerId = a.header.customerId,
                    totalPrice = new Currency(a.header.totalPrice),
                    items = a.detail,
                    status = a.header.status
                })).
                ToList();

            return orderDetailDTO;
        }
        */
        public void Save(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
