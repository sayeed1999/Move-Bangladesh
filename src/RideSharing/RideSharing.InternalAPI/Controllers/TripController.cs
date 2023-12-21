using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using RideSharing.Application;
using Sayeed.Generic.OnionArchitecture.Controller;

namespace RideSharing.API
{
    [Route("api/v1/trips")]
    [ApiController]
    public class TripController : BaseController<Trip>
    {

        public TripController(ITripService tripService) : base(tripService)
        {

        }
    }
}