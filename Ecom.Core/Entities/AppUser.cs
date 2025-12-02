using Microsoft.AspNetCore.Identity;
namespace Ecom.Core.Entities
{
    public class AppUser:IdentityUser
    {
        public Address Adress { get; set; }
        public string DispalyName { get; set; }
    } 
}
