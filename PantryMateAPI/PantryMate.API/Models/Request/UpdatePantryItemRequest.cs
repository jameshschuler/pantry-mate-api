using System.ComponentModel.DataAnnotations;

namespace PantryMate.API.Models.Request
{
    public class UpdatePantryItemRequest
    {
        [MinLength(1, ErrorMessage = "Must specify at least one item.")]
        public PantryItemRequest[] Items { get; set; }
    }
}
