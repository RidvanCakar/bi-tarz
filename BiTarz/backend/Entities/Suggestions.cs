using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities{

    [Table("Suggestions")]
    public class Suggestions{
        
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } = 0;
        
        [ForeignKey("UserId")]
        public User? User { get; set; } = null!;

        public int OutfitId { get; set; } = 0;
        
        [ForeignKey("Outfit")]
        public Outfit? Outfit { get; set; } = null!;

        public int ProductId { get; set; } = 0;

        [ForeignKey("ProductId")]

        public Product? product { get; set; } = null!;
            
    }
}