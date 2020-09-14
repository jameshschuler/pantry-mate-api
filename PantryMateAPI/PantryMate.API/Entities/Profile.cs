namespace PantryMate.API.Entities
{
    public class Profile : BaseEntity
    {
        public int ProfileID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public int AccountID { get; set; }
    }
}
