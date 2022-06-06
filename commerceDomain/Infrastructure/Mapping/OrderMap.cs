using commerceDomain.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;


namespace commerceDomain.Infrastructure.Mapping
{
    public class OrderMap : EntityTypeConfiguration<OrderDTO>
    {

        public OrderMap()
        {
            this.HasKey(t => t.id);
            this.ToTable("TrOrder");
            this.Property(t => t.id).HasColumnName("Id");
            this.Property(t => t.totalPrice).HasColumnName("TotalPrice");
            this.Property(t => t.customerId).HasColumnName("CustomerId");
            this.Property(t => t.status).HasColumnName("Status");

        }
    }
}
