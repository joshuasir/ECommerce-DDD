using commerceDomain.Infrastructure.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure.Mapping
{
    public class PaymentMap : EntityTypeConfiguration<PaymentDTO>
    {
        public PaymentMap()
        {
            this.HasKey(t => t.id);
            this.ToTable("TrPayment");
            this.Property(t => t.id).HasColumnName("Id");
            this.Property(t => t.orderId).HasColumnName("OrderId");
            this.Property(t => t.customerId).HasColumnName("CustomerId");
            this.Property(t => t.status).HasColumnName("Status");
            this.Property(t => t.total).HasColumnName("Total");
            this.Property(t => t.paidAt).HasColumnName("PaidAt");
        }
    }
}
