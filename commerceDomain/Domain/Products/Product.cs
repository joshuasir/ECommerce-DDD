using commerceDomain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain
{
    public class Product : AggregateRoot<Guid>
    {
        public Product(Guid id,string productName,string productDesc, Category category, Size size, Image image, Currency price) : base(id){
            this.id = id;
            this.productName = productName;
            this.productDescription = productDesc;
            this.categoryDetail = category;
            this.productSize = size;
            this.productImage = image;
            this.productPrice = price;

        }

        public string productName { get; private set; }
        public string productDescription { get; private set; }

        public Category categoryDetail { get; private set; }
        public Size productSize { get; private set; }
        public Image productImage { get; private set; }
        public Currency productPrice { get; private set; }
        public DateTime? createdAt { get; private set; } = DateTime.Now;

        public void SetPrice(int value)
        {
            if (value<=0)
                throw new ArgumentNullException(nameof(value));

            this.productPrice = new Currency(value);
        }

        public void SetSize(char value)
        {
            if (value=='\0')
                throw new ArgumentNullException(nameof(value));

            this.productSize = new Size(value);
        }
        public void SetImage(string value,string title)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));

            this.productImage = new Image(value,title);
        }
        public void SetCategory(Category value)
        {
            if (value==null)
                throw new ArgumentNullException(nameof(value));

            this.categoryDetail = value;
        }
        public static Product CreateNew(string name, string des, Guid categoryId, char size, string url, int price)
        {
            return new Product(Guid.NewGuid(), name, des, new Category(categoryId), new Size(size), new Image(url, name), new Currency(price));
        }
        public static Product CreateNew(Guid id,string name, string des, Guid categoryId, char size, string url, int price)
        {
            return new Product(id, name, des, new Category(categoryId), new Size(size), new Image(url, name), new Currency(price));
        }

    }
}
