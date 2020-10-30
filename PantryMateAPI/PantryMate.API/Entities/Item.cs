using System.Collections.Generic;

namespace PantryMate.API.Entities
{
    public class Item : BaseEntity
    {
        public Item()
        {
            PantryItems = new List<PantryItem>();
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public int? BrandId { get; set; }
        public decimal? Price { get; set; }
        public int? UnitOfMeasureId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
        public virtual ICollection<PantryItem> PantryItems { get; set; }
    }
}
