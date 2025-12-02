using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : BaseController
    {
        public WishListController(IUnitOfWork unitOfWork, IMapper mapper ) : base(unitOfWork, mapper)
        {
        }


        [HttpGet("Get-All-WishList")]
        public async Task<IActionResult> GetAllUserWishListProduc()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var item = await _unitOfWork.wishListRepository.GetAllWisListForUserAsync( email);
            return Ok(item);
        }


        [HttpPost("Add-Wishlist-Product")]

        public async Task<IActionResult> AddWishListProduc(WishListDTO wishListDTO)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var item = await _unitOfWork.wishListRepository.CreateWishListAsync(wishListDTO, email);

            return Ok(item);
        }
        [HttpDelete("Delete-Wishlist-Product")]

        public async Task<IActionResult> DeleteWishListProduc(int id)
        {
            var item = await _unitOfWork.wishListRepository.DeleteWishListAsync(id);

            return Ok(item);

        }
    }
}
