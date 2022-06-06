using commerceDomain.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.Mapping
{
    public class OrderDetailMap : EntityTypeConfiguration<OrderDetailDTO>
    {

        public OrderDetailMap()
        {
            this.HasKey(t => t.orderDetailId);
            this.ToTable("TrOrderDetail");
            this.Property(t => t.orderDetailId).HasColumnName("Id");
            this.Property(t => t.orderId).HasColumnName("OrderId");
            this.Property(t => t.totalPrice).HasColumnName("TotalPrice");
            this.Property(t => t.quantity).HasColumnName("Quantity");
            this.Property(t => t.totalPrice).HasColumnName("TotalPrice");

        }
    }
}
