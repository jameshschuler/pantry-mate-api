namespace PantryMate.API.Entities
{
    public class Inventory : BaseEntity
    {
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
    }
}
