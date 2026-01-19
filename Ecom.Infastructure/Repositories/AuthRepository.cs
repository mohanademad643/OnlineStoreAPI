
using Ecom.Application.DTO.Adminstration;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Application.Sharing;
using Ecom.Core.Services;
using Ecom.Domain.Entities;
using Ecom.Infastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecom.infrastructure.Repositories
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        private readonly IGenerateToken _generateToken;

        public AuthRepository(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken generateToken, AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _generateToken = generateToken;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<string> RegisterAsync(RegisterDTO registerDTO)
        {
            if (registerDTO == null) {
                return null;
            }

            if (await _userManager.FindByNameAsync(registerDTO.UserName) is not null)
            {
                return "This User Name is already registerd";
            }
            if (await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return "This Email is already registerd";
            }

            AppUser user = new()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                DispalyName = registerDTO.DisplayName,

            };
             await _userManager.AddToRoleAsync(user,"User");
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded is not true)
            {
                return result.Errors.ToList()[0].Description;
            }
            //send Active Email
            string Token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendEmail(user.Email, Token, "active", "ActiveEmail", "Please Active Your Email clik button to Active");
            await _userManager.AddToRoleAsync(user, "User");
            return "done";
        }

        public async Task SendEmail(string email, string code, string component, string subject, string message)
        {
            var result = new EmailDTO(email, "mohannad.emad535@gmail.com", subject,
                EmailStringBody.Send(email, code, component, message));

            await _emailService.SendEmail(result);
        }

        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return null;
            }

            var FindUser = await _userManager.FindByEmailAsync(loginDTO.Email);


            if (!FindUser.EmailConfirmed)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(FindUser);
                //send Active Email
                await SendEmail(FindUser.Email, token,
                    "active", "ActiveEmail", "Please Active Your Email click button to Active");

                return "Please Confirm Your Email First , We Send Activate To Your Email";
            }
            var result = await _signInManager.CheckPasswordSignInAsync(FindUser, loginDTO.Password, true);

            if (result.Succeeded)
            {
                return _generateToken.GetAndGenerateToken(FindUser);
            }

            return "Please Check Your Email And Password , something went wrong";
        }

        public async Task<bool> SendEmailForforgetPassword(string email)
        {
            var FindUser = await _userManager.FindByEmailAsync(email);

            if (FindUser is null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(FindUser);
            await SendEmail(FindUser.Email, token, "Reset-Password", "Reset-Password", " click button to Reset Your Password");

            return true;
        }

        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var FindUser = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);

            if (FindUser is null)
            {
                return "User not found";
            }
            if (resetPasswordDTO.Password is null)
            {
                return "password not found";
            }
            var result = await _userManager.ResetPasswordAsync(FindUser, resetPasswordDTO.Token, resetPasswordDTO.Password);

            if (result.Succeeded) {
                return "done";

            }
            return result.Errors.ToList()[0].Description;
        }

        public async Task<bool> ActiveAccount(ActiveAccountDTO accountDTO)
        {
            var findUser = await _userManager.FindByEmailAsync(accountDTO.Email);
            if (findUser is null)
            {
                return false;
            }

            var reslt = await _userManager.ConfirmEmailAsync(findUser, accountDTO.Token);
            if (reslt.Succeeded)
                return true;

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(findUser);
            await SendEmail(findUser.Email, token, "active", "ActiveEmail", "Please active your email, click on button to active");

            return false;
        }

        public async Task<bool> UpdateAddress(string email, Address address)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser is null)
            {
                return false;
            }

            var Myadress = await _context.Addresses.AsNoTracking().
                FirstOrDefaultAsync(m => m.AppUserId == findUser.Id);
            if (Myadress is null)
            {
                address.AppUserId = findUser.Id;
                await _context.Addresses.AddAsync(address);
            }
            else
            {
                _context.Entry(Myadress).State = EntityState.Detached;
                address.Id = Myadress.Id;
                address.AppUserId = Myadress.AppUserId;
                _context.Addresses.Update(address);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Address> getUserAddress(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            var address = await _context.Addresses.FirstOrDefaultAsync(m => m.AppUserId == User.Id);

            return address;
        }

       
    }
}
