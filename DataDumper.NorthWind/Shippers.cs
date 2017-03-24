using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataDumper.NorthWind
{
    public class Shippers
    {
        [Key]
        public int ShipperId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string Phone { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}