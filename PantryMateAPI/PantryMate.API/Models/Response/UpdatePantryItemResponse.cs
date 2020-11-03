namespace PantryMate.API.Models.Response
{
    public class UpdatePantryItemResponse
    {
        public int TotalItemCount { get; set; }
        public int UpdatedItemCount { get; set; }

        public string Message
        {
            get
            {
                if (TotalItemCount == UpdatedItemCount)
                {
                    return $"Successfully updated {UpdatedItemCount} item(s).";
                }
                else
                {
                    return $"Successfully updated {UpdatedItemCount} item(s) out of {TotalItemCount}.";
                }
            }
        }
    }
}
