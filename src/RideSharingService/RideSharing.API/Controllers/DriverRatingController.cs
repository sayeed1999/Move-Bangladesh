using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using RideSharing.Service;
using Sayeed.Generic.OnionArchitecture.Controller;

namespace RideSharing.API
{
    [Route("api/v1/driver-ratings")]
    [ApiController]
    public class DriverRatingController : BaseController<DriverRating>
    {
        public DriverRatingController(IDriverRatingService baseService) : base(baseService)
        {
        }
    }
}