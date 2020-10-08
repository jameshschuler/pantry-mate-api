using System.ComponentModel.DataAnnotations;

namespace PantryMate.API.Models.Request
{
    public class CreatePantryRequest
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [MinLength(1, ErrorMessage = "Name must be at least 1 character.")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }
    }
}
