using System;

namespace PantryMate.API.Models.Response
{
    public class PantryResponse
    {
        public int PantryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedON { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
