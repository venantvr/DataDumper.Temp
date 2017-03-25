using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tools.DataDumper.NorthWind
{
    public class Territories
    {
        [Key]
        public string TerritoryId { get; set; }

        [Required]
        public string TerritoryDescription { get; set; }

        public int RegionId { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
        public virtual Regions Region { get; set; }
    }
}