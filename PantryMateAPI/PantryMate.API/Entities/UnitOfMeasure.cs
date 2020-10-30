namespace PantryMate.API.Entities
{
    public class UnitOfMeasure : BaseEntity
    {
        public int UnitOfMeasureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public int AccountId { get; set; }
    }
}
