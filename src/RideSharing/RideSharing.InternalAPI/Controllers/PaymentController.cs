using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using Sayeed.Generic.OnionArchitecture.Controller;
using RideSharing.Application.Abstractions;

namespace RideSharing.API
{
    [Route("api/v1/payments")]
    [ApiController]
    public class PaymentController : BaseController<Payment>
    {
        public PaymentController(IPaymentRepository baseService) : base(baseService)
        {
        }
    }
}