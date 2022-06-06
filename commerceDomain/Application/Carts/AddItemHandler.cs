using commerceDomain.Application.Carts.RequestModel;
using commerceDomain.Domain.Carts;
using commerceDomain.Domain.Carts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Carts
{
    public class AddItemHandler
    {
        private ICartRepository _cartRepository;

        public AddItemHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public void AddItem(ItemCartRequest newItem)
        {
            var cart = _cartRepository.FindByCustomerId(newItem.customerId);

            cart.Changes.Add(new AddItemEvent(newItem.quantity,newItem.price,newItem.product));
            _cartRepository.Update(cart);
        }
    }
}
