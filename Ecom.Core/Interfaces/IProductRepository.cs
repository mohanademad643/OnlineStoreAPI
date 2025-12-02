

using Ecom.Core.DTOs;
using Ecom.Core.Entities.Products;
using Ecom.Core.Sharing;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<ReturnProductDTO> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(AddProductDTO addProductDTO);
        Task<bool> UpdateAsync(UpdateProductDTO UpdateProductDTO);
        Task DeleteAsync(Product product);
    }
}
