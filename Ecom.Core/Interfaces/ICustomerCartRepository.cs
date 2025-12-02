

using Ecom.Core.Entities;

namespace Ecom.Core.Interfaces
{
    public interface ICustomerCartRepository
    {
        Task<CustomerCart> GetCartAsync(string id);
        Task<CustomerCart> UpdateCartAsync(CustomerCart cart);
        Task<bool> DeleteCartAsync(string id);
    }
}
