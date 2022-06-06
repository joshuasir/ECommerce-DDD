using commerceDomain.Application.Carts.RequestModel;
using commerceDomain.Domain.Carts;
using commerceDomain.Domain.Carts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Carts
{
    public class RemoveQtyHandler
    {
        private ICartRepository _cartRepository;

        public RemoveQtyHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public void RemoveQty(ItemCartRequest newItem)
        {
            var cart = _cartRepository.FindByCustomerId(newItem.customerId);

            cart.Changes.Add(new UpdQtyEvent(newItem.product.id,newItem.quantity));
            _cartRepository.Update(cart);
        }
    }
}
