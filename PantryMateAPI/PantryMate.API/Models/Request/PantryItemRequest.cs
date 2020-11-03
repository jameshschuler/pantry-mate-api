using System.ComponentModel.DataAnnotations;

namespace PantryMate.API.Models.Request
{
    public class PantryItemRequest
    {
        public int ItemId { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public double? CurrentQuantity { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public double? MinimumQuantity { get; set; }
    }
}
