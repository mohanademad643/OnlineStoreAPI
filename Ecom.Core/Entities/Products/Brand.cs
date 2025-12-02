

namespace Ecom.Core.Entities.Products
{
    public class Brand : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
