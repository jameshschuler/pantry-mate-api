using System;

namespace PantryMate.API.Entities
{
    public class Account : BaseEntity
    {
        public int AccountId { get; set; }
        public bool Active { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLogin { get; set; }

        public Profile Profile { get; set; }
    }
}
