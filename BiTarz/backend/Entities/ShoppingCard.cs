using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{
    [Table("ShoppingCards")]
    public class ShoppingCard{

        [Key]
        public int Id { get; set; }

        public int Price {get;set;}
        
        public int UserId { get; set; } = 0;
        
        [ForeignKey("UserId")]
        public User? User { get; set; } = null!;
        
        [ForeignKey("Product")]
        public Product? Product { get; set; } = null!;
        
        public int Quantity { get; set; } = 1;

    }
}