using commerceDomain.Application.Carts.RequestModel;
using commerceDomain.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Carts
{
    public class GetCart
    {
        private ICartRepository _cartRepository;

        public GetCart(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Cart GetCustomerCart(CartRequest cart)
        {
            var cartByCustomer = _cartRepository.FindByCustomerId(cart.customerId);

            return (cartByCustomer == null ? _cartRepository.Create(Cart.CreateNew(cart.customerId)): cartByCustomer);
        }
    }
}
