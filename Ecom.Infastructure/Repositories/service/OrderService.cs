using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities.Order;
using Ecom.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.infrastructure.Repositories.service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<Orders> CreateOrderAsync(OrderDTO orderDTO, string BuyerEmail)
        {
            var cart = await _unitOfWork.CustomerCartRepository.GetCartAsync(orderDTO.cartId);

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var item in cart.cartItems) {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);

                OrderItem orderitem = new(product.Id, item.Image, product.Name, item.Price, item.Quantity);

                orderItems.Add(orderitem);
            }
            var deliveryMethod = await _context.DeliveryMethods.FirstOrDefaultAsync(m => m.Id == orderDTO.deliveryMethodId);
            var subTotal = orderItems.Sum(m => m.Price * m.Quantity);
            var ship = _mapper.Map<ShippingAddress>(orderDTO.ShippingAddress);
            var ExsistOrders = await _context.Orders.Where(m=>m.PaymentIntentId == cart.PaymentIntentId).FirstOrDefaultAsync();
            if (ExsistOrders is not null)
            {
                _context.Orders.Remove(ExsistOrders);
              await  _paymentService.CreateOrUpdateAsync(cart.PaymentIntentId, deliveryMethod.Id);
            }
            var order = new Orders(BuyerEmail, subTotal, ship, deliveryMethod,orderItems,cart.PaymentIntentId);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
           await _unitOfWork.CustomerCartRepository.DeleteCartAsync(orderDTO.cartId);
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync()
        {
          return  await _context.DeliveryMethods.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<OrderResponse>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
           var orders = await _context.Orders.Where(m=>m.BuyerEmail ==  BuyerEmail)
                 .Include(m => m.orderItems).Include(m => m.deliveryMethod)
                .ToListAsync();
            var result =  _mapper.Map<IReadOnlyList<OrderResponse>>(orders);
            return result;
        }

        public async Task<OrderResponse> GetOrdersById(int id, string BuyerEmail)
        {
            var order = await _context.Orders.Where(m => m.Id == id && m.BuyerEmail == BuyerEmail)
                .Include(m=>m.orderItems).Include(m => m.deliveryMethod)
                .FirstOrDefaultAsync();
            var result = _mapper.Map<OrderResponse>(order);
            return result;

        }
    }
}
