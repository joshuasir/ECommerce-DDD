using commerceDomain.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<CustomerDTO>
    {
        public CustomerMap()
        {
            this.HasKey(t => t.Id);
            this.ToTable("MsCustomer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.name).HasColumnName("Name");
            this.Property(t => t.email).HasColumnName("Email");
            this.Property(t => t.address).HasColumnName("Address");
            this.Property(t => t.newsLetter).HasColumnName("Subscribe");
            this.Property(t => t.stsrc).HasColumnName("Stsrc");
        }
    }
}
