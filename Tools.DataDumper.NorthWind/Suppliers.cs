using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tools.DataDumper.NorthWind
{
    public class Suppliers
    {
        public Suppliers()
        {
            AddressDetails = new AddressDetails();
        }

        [Key]
        public int SupplierId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public AddressDetails AddressDetails { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}