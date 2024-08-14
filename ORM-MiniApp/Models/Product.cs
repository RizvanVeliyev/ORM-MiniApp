using ORM_MiniApp.Models.Common;

namespace ORM_MiniApp.Models
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set;}
        public DateTime UpdatedAt { get; set;}
    }
}
