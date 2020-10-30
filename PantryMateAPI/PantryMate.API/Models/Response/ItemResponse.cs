namespace PantryMate.API.Models.Response
{
    public class ItemResponse
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string UnitOfMeasure { get; set; }
        public string AbbreviatedUnitOfMeasure { get; set; }
    }
}
