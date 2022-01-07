using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RentACarModel
{
    public partial class RentACarEntitiesModel : DbContext
    {
        public RentACarEntitiesModel()
            : base("name=RentACarEntitiesModel")
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Penalty> Penalties { get; set; }
        public virtual DbSet<RentOrder> RentOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasMany(e => e.RentOrders)
                .WithOptional(e => e.Car)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Penalties)
                .WithOptional(e => e.Customer)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.RentOrders)
                .WithOptional(e => e.Customer)
                .WillCascadeOnDelete();
        }
    }
}
