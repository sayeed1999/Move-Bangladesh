using Microsoft.AspNetCore.Mvc;
using RideSharing.Entity;
using RideSharing.Service;

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