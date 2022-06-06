using commerceDomain.Application.Carts.RequestModel;
using commerceDomain.Domain.Carts;
using commerceDomain.Domain.Carts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Carts
{
    public class CartCheckoutHandler
    {
        private ICartRepository _cartRepository;

        public CartCheckoutHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public void Checkout(ItemCartRequest cartRequest)
        {
            var cart = _cartRepository.FindByCustomerId(cartRequest.customerId);

            if (!cartRequest.id.Equals(cart.id)) return;

            cart.Changes.Add(new CartCheckoutEvent(cartRequest.customerId, cartRequest.id));
            _cartRepository.Update(cart);
        }
    }
}
