using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace DataDumper.NorthWind
{
    public class Customers
    {
        public Customers()
        {
            AddressDetails = new AddressDetails();
        }

        [Key]
        public string CustomerId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public AddressDetails AddressDetails { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<CustomerDemographics> CustomerDemographics { get; set; }
    }

    public class CustomersConfiguration : EntityTypeConfiguration<Customers>
    {
        public CustomersConfiguration()
        {
            HasMany(x => x.CustomerDemographics)
                .WithMany(x => x.Customers)
                .Map(m => m.MapLeftKey("CustomerId")
                    .MapRightKey("CustomerTypeId")
                    .ToTable("CustomerCustomerDemo"));
        }
    }
}