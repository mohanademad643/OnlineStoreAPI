

using Ecom.Core.DTOs;
using Ecom.Core.Entities;

namespace Ecom.Core.Interfaces
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
