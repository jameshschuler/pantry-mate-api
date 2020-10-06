namespace PantryMate.API.Entities
{
    public class InventoryItem
    {
        public int ItemId { get; set; }
        public int InventoryId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
