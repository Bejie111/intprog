using System.ComponentModel.DataAnnotations;

namespace BreadBee.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Optional account
        public int? UserId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalAmount { get; set; }

        // Navigation
        public List<OrderItem> Items { get; set; }
    }
}
