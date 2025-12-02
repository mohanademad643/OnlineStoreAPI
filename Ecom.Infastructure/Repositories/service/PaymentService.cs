using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities;
using Ecom.Domain.Entities.Order;
using Ecom.Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;


namespace Ecom.infrastructure.Repositories.service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _Unitwork;
        private readonly IConfiguration configuration;
        private readonly AppDbContext _context;

        public PaymentService(IUnitOfWork unitwork, IConfiguration configuration, AppDbContext context)
        {
            _Unitwork = unitwork;
            this.configuration = configuration;
            _context = context;
        }

        public async Task<CustomerCart> CreateOrUpdateAsync(string CartId,int? deliveryMethodId)
        {
            var cart = await _Unitwork.CustomerCartRepository.GetCartAsync(CartId);
            StripeConfiguration.ApiKey = configuration["StripeSetting:SecretKey"];
            decimal shippingPrice = 0m;

            if (deliveryMethodId.HasValue)
            {
                var delivery = await _context.DeliveryMethods.AsNoTracking()
                   .FirstOrDefaultAsync(m => m.Id == deliveryMethodId.Value);
                shippingPrice = delivery.Price;
            }
            foreach (var item in cart.cartItems)
            {
                var product = await _Unitwork.ProductRepository.GetByIdAsync(item.Id);
                item.Price = product.NewPrice;
            }
            PaymentIntentService paymentIntentService = new();
            PaymentIntent _intent;
            if (string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)cart.cartItems.Sum(m => m.Quantity * (m.Price * 100)) + (long)(shippingPrice * 100),

                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                _intent = await paymentIntentService.CreateAsync(option);
                cart.PaymentIntentId = _intent.Id;
                cart.ClientSecret = _intent.ClientSecret;
            }
            else
            {
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)cart.cartItems.Sum(m => m.Quantity * (m.Price * 100)) + (long)(shippingPrice * 100),

                };
                await paymentIntentService.UpdateAsync(cart.PaymentIntentId, option);
            }
            await _Unitwork.CustomerCartRepository.UpdateCartAsync(cart);
            return cart;
        }

        public async Task<Orders> UpdateOrderFaild(string PaymentInten)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.PaymentIntentId == PaymentInten);
            if (order is null)
            {
                return null;
            }
            order.status = Status.PaymentFaild;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Orders> UpdateOrderSuccess(string PaymentInten)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.PaymentIntentId == PaymentInten);
            if (order is null)
            {
                return null;
            }
            order.status = Status.PaymentReceived;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
    
}
