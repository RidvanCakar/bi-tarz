using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } = 0;

        // public decimal Price { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; } = null!;
        public int ProductId { get; set; } = 0;

        [ForeignKey("ProductId")]
        public Product? Product { get; set; } = null!;
        public int Quantity { get; set; } = 0;
        public string ImageUrl { get; set; } = string.Empty;
        public string OrderNumber { get; set;} = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}