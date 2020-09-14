using System.ComponentModel.DataAnnotations;

namespace PantryMate.API.Models.Request
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters in length.")]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters in length.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
