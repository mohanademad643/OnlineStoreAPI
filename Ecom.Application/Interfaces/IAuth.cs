

using Ecom.Application.DTO.Adminstration;
using Ecom.Application.DTOs;
using Ecom.Domain.Entities;

namespace Ecom.Application.Interfaces
{
    public interface IAuth
    {
        Task<string> RegisterAsync(RegisterDTO registerDTO);
        Task<string> LoginAsync(LoginDTO loginDTO);
        Task<bool> SendEmailForforgetPassword(string email);
        Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<bool> ActiveAccount(ActiveAccountDTO accountDTO);
        Task<bool> UpdateAddress(string email, Address adress);
        Task<Address> getUserAddress(string email);
       


    }
}
