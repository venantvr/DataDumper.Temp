using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tools.DataDumper.NorthWind
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}