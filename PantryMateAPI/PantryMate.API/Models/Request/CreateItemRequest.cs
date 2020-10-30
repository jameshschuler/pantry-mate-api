using System.ComponentModel.DataAnnotations;

namespace PantryMate.API.Models.Request
{
    public class CreateItemRequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "Name must be at least 1 character.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Price cannot contain more than 2 decimal places." )]
        [Range(0.00, 999999999, ErrorMessage = "Price cannot be less than 0.00")]
        public decimal Price { get; set; } = 0.00M;

        public int? UnitOfMeasureId { get; set; }

        public int? BrandId { get; set; }
    }
}
