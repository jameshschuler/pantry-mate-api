using System.ComponentModel.DataAnnotations;

namespace PantryMate.API.Models.Request
{
    public class UpdateProfileRequest
    {
        [MaxLength(100, ErrorMessage = "First Name cannot be longer than 100 characters.")]
        public string FirstName { get; set; }

        [MaxLength(100, ErrorMessage = "Last Name cannot be longer than 100 characters.")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "Email Address cannot be longer than 100 characters.")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [MaxLength(20, ErrorMessage = "Phone Number cannot be longer than 20 characters.")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
