using System.ComponentModel.DataAnnotations;

namespace PantryMate.API.Models.Request
{
    public class UpdatePantryRequest
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }

        public bool IsShared { get; set; }
    }
}
