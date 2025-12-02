using Ecom.Core.DTOs;

namespace Ecom.Core.Interfaces
{
    public interface IRating
    {
        Task<bool> AddRatingAsync(RatingDTO ratingDTO, string email);
        Task<IReadOnlyList<ReturnRatingDTO>> GetAllRatingForProductAsync(int productId);
    }
}
