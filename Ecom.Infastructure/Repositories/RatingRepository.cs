
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Ecom.Domain.Entities.Products;
using Ecom.Infastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecom.infrastructure.Repositories
{
    public class RatingRepository:IRating
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public RatingRepository(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AddRatingAsync(RatingDTO ratingDTO, string email)
        {
            var finduser = await _userManager.FindByEmailAsync(email);

            if (await _context.Ratings.AsNoTracking().AnyAsync(m => m.AppUserId == finduser.Id && m.ProductId == ratingDTO.productId))
            {
                return false;
            }

            var rating = new Rating()
            {
                AppUserId = finduser.Id,
                ProductId = ratingDTO.productId,
                Stars = ratingDTO.stars,
                content = ratingDTO.content,

            };
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == ratingDTO.productId);

            var ratings = await _context.Ratings.AsNoTracking().Where(m => m.ProductId == product.Id).ToListAsync();

            if (ratings.Count > 0)
            {
                double average = ratings.Average(m => m.Stars);
                double roundedReview = Math.Round(average * 2, mode: MidpointRounding.AwayFromZero) / 2;
                product.rating = roundedReview;
            }
            else
            {
                product.rating = ratingDTO.stars;
            }
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IReadOnlyList<ReturnRatingDTO>> GetAllRatingForProductAsync(int productId)
        {
            var ratings = await _context.Ratings.Include(m => m.AppUser)
                 .AsNoTracking().Where(m => m.ProductId == productId).ToListAsync();

            return ratings.Select(m => new ReturnRatingDTO
            {
                content = m.content,
                ReviewTime = m.Review,
                stars = m.Stars,
                userName = m.AppUser.UserName,
            }).ToList();
        }
    }
}
