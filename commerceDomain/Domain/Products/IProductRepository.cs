using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Products
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(Guid id);
        Product FindBy(Guid Id);
        List<Product> GetCatalogue();
        List<Product> GetCatalogueByCategoryId(Guid id);
        void SendNewsLetter(List<Product> products);
    }
}
