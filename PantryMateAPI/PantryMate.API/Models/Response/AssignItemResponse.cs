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
                    return $"Successfully assigned {AssignedItemCount} item(s).";
                } 
                else
                {
                    return $"Successfully assigned {AssignedItemCount} item(s) out of {TotalItemCount}.";
                }
            }
        }
    }
}
