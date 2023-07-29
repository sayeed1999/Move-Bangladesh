using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Entity;
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
