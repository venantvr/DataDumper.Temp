using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataDumper.NorthWind
{
    [Table("Region")]
    public class Regions
    {
        [Key]
        public int RegionId { get; set; }

        [Required]
        public string RegionDescription { get; set; }

        public virtual ICollection<Territories> Territories { get; set; }
    }
}