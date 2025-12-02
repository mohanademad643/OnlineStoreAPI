
using Ecom.Core.DTOs;
using Ecom.Core.Entities.Order;

namespace Ecom.Core.Services
{
    public interface IOrderService
    {
        Task<Orders> CreateOrderAsync(OrderDTO orderDTO, string BuyerEmail);
        Task<IReadOnlyList<OrderResponse>> GetAllOrdersForUserAsync( string BuyerEmail);

        Task<OrderResponse> GetOrdersById(int id, string BuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync();
    }
}
