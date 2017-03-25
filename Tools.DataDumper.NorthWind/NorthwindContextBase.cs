using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Tools.DataDumper.NorthWind
{
    public class NorthWindContextBase : DbContext
    {
        public NorthWindContextBase()
        {
        }

        public NorthWindContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public ObjectContext ObjectContext => (this as IObjectContextAdapter).ObjectContext;

        public DbSet<Categories> Categories { get; set; }
        public DbSet<CustomerDemographics> CustomerDemographics { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Territories> Territories { get; set; }
    }
}