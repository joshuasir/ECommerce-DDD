using commerceDomain.Domain;
using commerceDomain.Domain.Products;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Products
{
    public class GetProductList
    {
        private IProductRepository _productRepository;

        public GetProductList(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public List<Product> GetCatalogue()
        {
            return _productRepository.GetCatalogue();
        }
        public List<Product> GetCatalogueByCategoryId(Guid id)
        {
            return _productRepository.GetCatalogueByCategoryId(id);
        }

    }
}
