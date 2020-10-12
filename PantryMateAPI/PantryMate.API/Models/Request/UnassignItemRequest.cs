namespace PantryMate.API.Models.Request
{
    public class UnassignItemRequest
    {
        public int[] ItemIds { get; set; }

        public bool UnassignAllItems { get; set; }
    }
}
