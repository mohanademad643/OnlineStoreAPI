

using Ecom.Core.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.Entities
{
    public class WishList:BaseEntity<int>
    {


        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int ProdutId { get; set; }
        [ForeignKey(name: nameof(ProdutId))]
        public virtual Product Product { get; set; }

    }
}
