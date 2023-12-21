using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using RideSharing.Application;
using Sayeed.Generic.OnionArchitecture.Controller;

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