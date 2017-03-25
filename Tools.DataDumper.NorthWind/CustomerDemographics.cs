using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tools.DataDumper.NorthWind
{
    public class CustomerDemographics
    {
        [Key]
        public string CustomerTypeId { get; set; }

        public string CustomerDesc { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}