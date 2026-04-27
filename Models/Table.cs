using System.ComponentModel.DataAnnotations;

namespace qrmenusistemiuygulama18.Models
{
    public class Table
    {
        public int Id { get; set; }

        [Required]
        public int TableNumber { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
