using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Entity;
using RideSharing.Service;
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
