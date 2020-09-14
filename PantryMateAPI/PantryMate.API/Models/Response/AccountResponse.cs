namespace PantryMate.API.Models.Response
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public bool Active { get; set; }

        public ProfileResponse Profile { get; set; }
    }
}
