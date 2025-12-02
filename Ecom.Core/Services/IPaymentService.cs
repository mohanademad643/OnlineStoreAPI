

using Ecom.Core.Entities;
using Ecom.Core.Entities.Order;

namespace Ecom.Core.Services
{
    public interface IPaymentService
    {
        Task<CustomerCart> CreateOrUpdateAsync(string CartId, int? deliveryMethodId);
        Task<Orders> UpdateOrderSuccess(string PaymentInten);
        Task<Orders> UpdateOrderFaild(string PaymentInten);
    }
}
