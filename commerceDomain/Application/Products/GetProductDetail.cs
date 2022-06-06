using commerceDomain.Domain;
using commerceDomain.Domain.Products;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Products
{
    public class GetProductDetail
    {
        private IProductRepository _productRepository;

        public GetProductDetail(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product FindBy(Guid id)
        {
            return _productRepository.FindBy(id);
        }
    }
}
