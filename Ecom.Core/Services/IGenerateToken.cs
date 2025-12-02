
using Ecom.Core.Entities;

namespace Ecom.Core.Services
{
    public interface IGenerateToken
    {
        string GetAndGenerateToken(AppUser user);
    }
}
