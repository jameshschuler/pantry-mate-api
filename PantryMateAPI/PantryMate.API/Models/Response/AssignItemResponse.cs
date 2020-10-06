namespace PantryMate.API.Models.Response
{
    public class AssignItemResponse
    {
        public int TotalItemCount { get; set; }
        public int AssignedItemCount { get;set; }
        public string Message { get; set; }
    }
}
