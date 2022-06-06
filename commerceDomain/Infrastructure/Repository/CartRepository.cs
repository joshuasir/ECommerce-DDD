using commerceDomain.Domain;
using commerceDomain.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IEventStore _eventStore;
        public CartRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        private string StreamNameFor(Guid id)
        {
            // stream per-aggregate: {AggregateType}-{AggregateId}
            return string.Format("{0}-{1}", typeof(Cart).Name, id);
        }

        public Cart Create(Cart cart)
        {
            var streamName = StreamNameFor(cart.id);
            _eventStore.CreateNewStream(streamName, cart.Changes);

            return cart;
        }

        public void Update(Cart cart)
        {
            var streamName = StreamNameFor(cart.id);
            _eventStore.AppendEventsToStream(streamName, cart.Changes,null);
        }

        public Cart FindByCustomerId(Guid id)
        {
            var streamName = StreamNameFor(id);
            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;
            var stream = _eventStore.GetStream(
            streamName, fromEventNumber, toEventNumber
            );
            var cart = Cart.CreateNew(id);
            foreach (var @event in stream)
            {
                cart.Apply(@event);
            }
            return cart;
        }

    }
}
