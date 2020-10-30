namespace PantryMate.API.Models.Request
{
    public class PantryItemRequest
    {
        public int ItemId { get; set; }
        public double? CurrentQuantity { get; set; }
        public double? MinimumQuantity { get; set; }
    }
}
