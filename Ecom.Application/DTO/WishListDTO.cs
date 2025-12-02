
namespace Ecom.Application.DTOs
{
    public record WishListDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
    public record WishListResponse : WishListDTO
    {
        public int ProdutId { get; set; }
    }
}
