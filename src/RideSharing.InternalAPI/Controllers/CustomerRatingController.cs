using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using Sayeed.Generic.OnionArchitecture.Controller;
using RideSharing.Application.Abstractions;

namespace RideSharing.API
{
    [Route("api/v1/customer-ratings")]
    [ApiController]
    public class CustomerRatingController : BaseController<CustomerRating>
    {
        public CustomerRatingController(ICustomerRatingRepository baseService) : base(baseService)
        {
        }
    }
}