using System.Collections.Generic;

namespace PantryMate.API.Entities
{
    public class Pantry : BaseEntity
    {
        public Pantry()
        {
            PantryItems = new List<PantryItem>();
        }

        public int PantryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public bool IsShared { get; set; }

        public virtual ICollection<PantryItem> PantryItems { get; set; }
    }
}
