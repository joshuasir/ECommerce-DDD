using commerceDomain.Domain;
using commerceDomain.Domain.Products;
using commerceDomain.Domain.Products.Services;
using commerceDomain.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly CommerceDatabaseContext _commerceContext;

        public ProductRepository(CommerceDatabaseContext commerceContext)
        {
            _commerceContext = commerceContext;
        }

        public void Add(Product product)
        {

            var productDTO = new ProductDTO()
            {
                id = product.id,
                categoryId = product.categoryDetail.id,
                productDescription = product.productDescription,
                productImage = product.productImage.imageUrl,
                productName = product.productName,
                productPrice = product.productPrice.value,
                productSize = product.productSize.size
            };

            _commerceContext.Products.Add(productDTO);
        }

        public void SendNewsLetter(List<Product> products)
        {
            var newsLetter = new NewsLetter(products);

            _commerceContext.Customers.Where(a=>a.newsLetter=='A').ToList().ForEach(a=> {
                newsLetter.SendEmail(a.email,"New Arrival");
            });
        }

        public void Delete(Guid id)
        {
            var toDel = _commerceContext.Products.Find(id);
            toDel.stsrc = 'D';
        }


        public void Update(Product product)
        {
            var toUpd = _commerceContext.Products.Find(product.id);
            toUpd.productDescription = product.productDescription;
            toUpd.productImage = product.productImage.imageUrl;
            toUpd.productName = product.productName;
            toUpd.productPrice = product.productPrice.value;
            toUpd.productSize = product.productSize.size;
            toUpd.categoryId = product.categoryDetail.id;
        }

        public Product FindBy(Guid Id)
        {
            var productDTO = _commerceContext.Products.Find(Id);

            if (productDTO == null)
                throw new ArgumentNullException(nameof(productDTO));

            return Product.CreateNew(productDTO.id,productDTO.productName, productDTO.productDescription,productDTO.categoryId,productDTO.productSize, productDTO.productImage, productDTO.productPrice );
        }

        public List<Product> GetCatalogue()
        {
            return _commerceContext.Products.Select(a=> 
            Product.CreateNew(
                a.id, 
                a.productName, 
                a.productDescription, 
                a.categoryId, 
                a.productSize, 
                a.productImage, 
                a.productPrice
            )).ToList();
        }

        public List<Product> GetCatalogueByCategoryId(Guid id)
        {
            return _commerceContext.Products.
            Where(a=>a.categoryId == id).
            Select(a =>
            Product.CreateNew(
                a.id,
                a.productName,
                a.productDescription,
                a.categoryId,
                a.productSize,
                a.productImage,
                a.productPrice
            )).ToList();
        }
    }
}
