using System.ComponentModel.DataAnnotations;

namespace SecureShoppingApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}