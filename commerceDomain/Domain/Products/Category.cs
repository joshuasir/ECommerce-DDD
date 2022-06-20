using commerceDomain.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain
{

    public class Category : Entity<Guid>
    {
        private Category(Guid id, string categoryName, string categoryDescription)
        {
            base.id = id;
            this.categoryName = categoryName;
            this.categoryDescription = categoryDescription;
        }
        public Category(Guid id)
        {
            base.id = id;
        }

        public string categoryName { get; private set; }

        public string categoryDescription { get; private set; }

        public static Category CreateNew(string categoryName, string categoryDescription)
        {
            return new Category(Guid.NewGuid(), categoryName, categoryDescription);
        }
    }
}
