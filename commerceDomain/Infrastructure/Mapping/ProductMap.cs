using commerceDomain.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.Mapping
{
    public class ProductMap : EntityTypeConfiguration<ProductDTO>
    {
        public ProductMap()
        {
            this.HasKey(t => t.id);
            this.ToTable("MsProduct");
            this.Property(t => t.id).HasColumnName("Id");
            this.Property(t => t.productName).HasColumnName("Name");
            this.Property(t => t.productDescription).HasColumnName("Desc");
            this.Property(t => t.productPrice).HasColumnName("Price");
            this.Property(t => t.productSize).HasColumnName("Size");
            this.Property(t => t.productImage).HasColumnName("ImageURL");
            this.Property(t => t.categoryId).HasColumnName("CategoryId");
            this.Property(t => t.stsrc).HasColumnName("Stsrc");
        }
        
    }
}
