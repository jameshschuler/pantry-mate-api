namespace PantryMate.API.Entities
{
    public class PantryItem
    {
        public int ItemId { get; set; }
        public int PantryId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Pantry Pantry { get; set; }
    }
}
