
using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Core.Entities;
using Ecom.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.infrastructure.Repositories
{
    public class WishListRepository : IWishListRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public WishListRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WishList> CreateWishListAsync(WishListDTO wishListDTO, string UserEmail)
        {

            var wishlistitem = new WishList
            {
                UserEmail= UserEmail,
               Name = wishListDTO.Name,
            Description = wishListDTO.Description,
                Image = wishListDTO.Image,
              Price = wishListDTO.Price
            };
            await _context.WishLists.AddAsync(wishlistitem);
            await _context.SaveChangesAsync();
            return wishlistitem;
        }

        public async Task<bool> DeleteWishListAsync(int id)
        {
            var wishListItem = _context.WishLists.FirstOrDefault(x => x.Id == id);
             _context.Remove(wishListItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<WishListResponse>> GetAllWisListForUserAsync(string UserEmail)
        {
            var items = await _context.WishLists.Where(m => m.UserEmail == UserEmail)
                .ToListAsync();
            var withlistMap = _mapper.Map<IReadOnlyList<WishListResponse>>(items);
            return withlistMap;
        }

    }
}
