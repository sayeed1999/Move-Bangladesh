using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using Sayeed.Generic.OnionArchitecture.Controller;
using RideSharing.Application.Abstractions;

namespace RideSharing.API
{
    [Route("api/v1/trips")]
    [ApiController]
    public class TripController : BaseController<Trip>
    {

        public TripController(ITripRepository tripService) : base(tripService)
        {

        }
    }
}