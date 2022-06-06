using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Carts
{
    public interface ICartRepository
    {
        public Cart Create(Cart cart);
        public void Update(Cart cart);
        public Cart FindByCustomerId(Guid id);
    }
}
