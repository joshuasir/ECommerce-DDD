using commerceDomain.Application.Carts.RequestModel;
using commerceDomain.Domain.Carts;
using commerceDomain.Domain.Carts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Carts
{
    public class RemoveItemHandler
    {
        private ICartRepository _cartRepository;

        public RemoveItemHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public void RemoveItem(ItemCartRequest newItem)
        {
            var cart = _cartRepository.FindByCustomerId(newItem.customerId);

            cart.Changes.Add(new RemoveItemEvent(newItem.product.id));
            _cartRepository.Update(cart);
        }
    }
}
