namespace PantryMate.API.Models.Response
{
    public class UnassignItemResponse
    {
        public int TotalItemCount { get; set; }
        public int UnassignedItemCount { get; set; }
        public string Message
        {
            get
            {
                if (TotalItemCount == UnassignedItemCount)
                {
                    return $"Successfully unassigned {UnassignedItemCount} item(s).";
                }
                else
                {
                    return $"Successfully unassigned {UnassignedItemCount} item(s) out of {TotalItemCount}.";
                }
            }
        }
    }
}
