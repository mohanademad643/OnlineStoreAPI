using Ecom.Core.Entities;
using Ecom.Core.Services;
using Ecom.Domain.Entities;
using Ecom.infrastructure.Repositories.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<CustomerCart>> create(string CartId, int? deliveryId)
        {
            return await _paymentService.CreateOrUpdateAsync(CartId, deliveryId);
        }
    }
}
