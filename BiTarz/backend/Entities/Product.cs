using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    [Table("Products")]
    public class Product{

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; } = null!;
    }
}