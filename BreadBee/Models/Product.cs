using System.ComponentModel.DataAnnotations;

namespace BreadBee.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public int Stock { get; set; }

        // Category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
