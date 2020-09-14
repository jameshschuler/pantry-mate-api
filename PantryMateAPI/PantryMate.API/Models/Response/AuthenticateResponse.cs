using PantryMate.API.Entities;

namespace PantryMate.API.Models.Response
{
    public class AuthenticateResponse
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Account account, string token)
        {
            AccountId = account.AccountId;
            Username = account.Username;
            Token = token;
        }
    }
}
