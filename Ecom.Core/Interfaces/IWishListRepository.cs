
using Ecom.Core.DTOs;
using Ecom.Core.Entities;

namespace Ecom.Core.Interfaces
{
    public interface IWishListRepository
    {
        Task<WishList> CreateWishListAsync(WishListDTO wishListDTO, string UserEmail);
        Task<IReadOnlyList<WishListResponse>> GetAllWisListForUserAsync(string UserEmail);
        Task<bool> DeleteWishListAsync(int id);
    }
}
