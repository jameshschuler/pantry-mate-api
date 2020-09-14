using System.ComponentModel.DataAnnotations;

namespace PantryMate.API.Entities
{
    public class Brand : BaseEntity
    {
        [Key]
        public int BrandId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
