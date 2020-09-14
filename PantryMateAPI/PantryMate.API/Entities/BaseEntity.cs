using System;

namespace PantryMate.API.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
