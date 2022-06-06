using commerceDomain.Application.Products.RequestModel;
using commerceDomain.Domain;
using commerceDomain.Domain.Products;
using commerceDomain.Domain.Products.Services;
using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Application.Products
{
    public class AddProduct
    {
        private IProductRepository _productRepository;
        private CommerceDatabaseContext _unitofWork;

        public AddProduct(IProductRepository productRepository, CommerceDatabaseContext unitofWork)
        {
            _productRepository = productRepository;
            _unitofWork = unitofWork;
        }

        public void Add(List<ProductRequest> prods)
        {
            prods.ForEach(prod =>
            {
                _productRepository.Add(Product.CreateNew(prod.name, prod.des, prod.categoryId, prod.size, prod.url, prod.price));
            });
            _productRepository.SendNewsLetter(prods.Select(prod=> Product.CreateNew(prod.name, prod.des, prod.categoryId, prod.size, prod.url, prod.price)).ToList());
            _unitofWork.SaveChanges();
        }
    }
}
