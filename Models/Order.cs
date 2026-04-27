using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using qrmenusistemiuygulama18.Models.Enums;

namespace qrmenusistemiuygulama18.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int TableId { get; set; }

        [ForeignKey("TableId")]
        public Table? Table { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public bool IsCompleted { get; set; } = false;

        public OrderStatus Status { get; set; } = OrderStatus.New;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
