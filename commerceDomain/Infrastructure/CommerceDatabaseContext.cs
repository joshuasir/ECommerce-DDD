using commerceDomain.Infrastructure.DataModel;
using commerceDomain.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Infrastructure
{
    public class CommerceDatabaseContext : DbContext
    {
        static CommerceDatabaseContext()
        {
            Database.SetInitializer<CommerceDatabaseContext>(null);
        }
        public CommerceDatabaseContext()
        : base("Name=CommerceDatabaseContext")
        {
        }
        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet<OrderDetailDTO> OrderDetails { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<PaymentDTO> Payments { get; set; }
        public DbSet<ProductDTO> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new PaymentMap());
            modelBuilder.Configurations.Add(new ProductMap());
        }

        public void Clear()
        {
            var context = ((IObjectContextAdapter)this).ObjectContext;
            var addedObjects = context
            .ObjectStateManager
            .GetObjectStateEntries(EntityState.Added);
            foreach (var objectStateEntry in addedObjects)
            {
                context.Detach(objectStateEntry.Entity);
            }
            var modifiedObjects = context.ObjectStateManager
            .GetObjectStateEntries(EntityState.Modified);
            foreach (var objectStateEntry in modifiedObjects)
            {
                context.Detach(objectStateEntry.Entity);
            }
       
        }
        
    }     
}
