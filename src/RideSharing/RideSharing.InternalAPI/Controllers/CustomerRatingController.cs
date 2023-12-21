using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using RideSharing.Service;
using Sayeed.Generic.OnionArchitecture.Controller;

namespace RideSharing.API
{
    [Route("api/v1/customer-ratings")]
    [ApiController]
    public class CustomerRatingController : BaseController<CustomerRating>
    {
        public CustomerRatingController(ICustomerRatingService baseService) : base(baseService)
        {
        }
    }
}