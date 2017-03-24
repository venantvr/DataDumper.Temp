using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataDumper.NorthWind
{
    [Table("Order Details")]
    public class OrderDetails
    {
        [Key, Column(Order = 0)]
        public int OrderId { get; set; }

        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public short Quantity { get; set; }

        [Required]
        public float Discount { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}