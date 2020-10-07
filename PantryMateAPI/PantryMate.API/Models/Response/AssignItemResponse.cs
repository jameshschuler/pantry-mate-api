namespace PantryMate.API.Models.Response
{
    public class AssignItemResponse
    {
        public int TotalItemCount { get; set; }
        public int AssignedItemCount { get;set; }
        public string Message 
        { 
            get 
            {
                if (TotalItemCount == AssignedItemCount)
                {
                    return $"Assigned {AssignedItemCount} items.";
                } 
                else
                {
                    return $"Assigned {AssignedItemCount} items out of {TotalItemCount}.";
                }
            }
        }
    }
}
