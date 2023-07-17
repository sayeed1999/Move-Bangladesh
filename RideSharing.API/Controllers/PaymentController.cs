using Microsoft.AspNetCore.Mvc;
using RideSharing.Entity;
using RideSharing.Service;

namespace RideSharing.API
{
    [Route("api/v1/payments")]
    [ApiController]
    public class PaymentController : BaseController<Payment>
    {
        public PaymentController(IPaymentService baseService) : base(baseService)
        {
        }
    }
}