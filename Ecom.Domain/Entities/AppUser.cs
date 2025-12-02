using Microsoft.AspNetCore.Identity;
namespace Ecom.Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public Address Adress { get; set; }
        public string DispalyName { get; set; }
    } 
}
