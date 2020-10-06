using System.Collections.Generic;

namespace PantryMate.API.Entities
{
    public class Inventory : BaseEntity
    {
        public Inventory()
        {
            InventoryItems = new List<InventoryItem>();
        }

        public int InventoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
