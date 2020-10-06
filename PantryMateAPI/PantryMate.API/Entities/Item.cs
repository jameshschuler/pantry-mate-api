using System.Collections.Generic;

namespace PantryMate.API.Entities
{
    public class Item : BaseEntity
    {
        public Item()
        {
            InventoryItems = new List<InventoryItem>();
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public int? BrandId { get; set; }
        public decimal? Price { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
