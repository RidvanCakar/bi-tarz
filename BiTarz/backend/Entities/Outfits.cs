using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Entities
{ 
    [Table("Outfits")]
    public class Outfit
    {
        
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; } = 0;

        [ForeignKey("UserId")]
        public User? User { get; set; } = null!;
        public string detectedType {get;set;} = string.Empty;
        public string detectedColor {get;set;}= string.Empty;
        public string detectedPattern {get;set;}= string.Empty;

    }
}