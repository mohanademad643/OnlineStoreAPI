using AutoMapper;
using Ecom.API.Helper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.API.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterDTO>> register(RegisterDTO registerDTO)
        {
            string result = await _unitOfWork.AuthRepository.RegisterAsync(registerDTO);
            if (result != "done")
            {
                return BadRequest(new ResponseAPI(400, result));
            }
            return Ok(new ResponseAPI(200, result));
        }

        [HttpGet("IsUserAuth")]
        public async Task<IActionResult> IsUserAuth()
        {

            return  User.Identity.IsAuthenticated ? Ok() : BadRequest();
        }
        [HttpGet("Logout")]
        public void logout()
        {

            Response.Cookies.Append("token", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(-1)
            });
        }
        [Authorize]
        [HttpGet("get-user-name")]
        public IActionResult GetUserName()
        {
            return Ok(new ResponseAPI(200, User.Identity.Name));
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            string result = await _unitOfWork.AuthRepository.LoginAsync(loginDTO);
            if (result.StartsWith("Please"))
            {
                return BadRequest(new ResponseAPI(400, result));
            }
            Response.Cookies.Append("token", result, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1),
                SameSite = SameSiteMode.None,
                IsEssential = true
            });

            return Ok(new ResponseAPI(200, result));
        }

        [HttpPost("active-account")]

        public async Task<IActionResult> ActiveAccount(ActiveAccountDTO accountDTO)
        {
            var result = await _unitOfWork.AuthRepository.ActiveAccount(accountDTO);

            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(400));
        }

        [HttpGet("send-email-forget-password")]

        public async Task<IActionResult> forget(string email)
        {
            var result = await _unitOfWork.AuthRepository.SendEmailForforgetPassword(email);

            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(400));
        }
        [HttpGet("get-address-for-user")]
        public async Task<IActionResult> getAddress()
        {
            var address = await _unitOfWork.AuthRepository.getUserAddress(User.FindFirst(ClaimTypes.Email).Value);
            var result = _mapper.Map<ShippingAddressDTO>(address);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> reset(ResetPasswordDTO resetPasswordDTO)
        {
            var result = await _unitOfWork.AuthRepository.ResetPassword(resetPasswordDTO);

            if(result == "done")
            {
                Ok(new ResponseAPI(200));
            }
            return BadRequest(new ResponseAPI(400));
        }

        [Authorize]
        [HttpPut("update-address")]
        public async Task<IActionResult> UpdateAddress(ShippingAddressDTO addressDTO)
        {
            var email =  User.FindFirst(ClaimTypes.Email)?.Value;
            var address = _mapper.Map<Address>(addressDTO);
            var result = await _unitOfWork.AuthRepository.UpdateAddress(email, address);
            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(400));
        }

       
    }
}
