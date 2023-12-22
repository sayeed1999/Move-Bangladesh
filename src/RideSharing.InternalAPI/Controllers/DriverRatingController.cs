using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using Sayeed.Generic.OnionArchitecture.Controller;
using RideSharing.Application.Abstractions;

namespace RideSharing.API
{
    [Route("api/v1/driver-ratings")]
    [ApiController]
    public class DriverRatingController : BaseController<DriverRating>
    {
        public DriverRatingController(IDriverRatingRepository baseService) : base(baseService)
        {
        }
    }
}