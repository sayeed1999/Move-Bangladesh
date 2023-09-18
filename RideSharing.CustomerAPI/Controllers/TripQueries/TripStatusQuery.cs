using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Common.Entities;
using RideSharing.Entity;
using RideSharing.Entity.Dtos;

namespace RideSharing.CustomerAPI.Controllers.TripCommands
{
    [Route("api/external/trips")]
    [ApiController]
    public class TripStatusQuery : ControllerBase
    {
        private readonly IMediator _mediator;

        public TripStatusQuery(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{tripId}/status")]
        public async Task<ActionResult<Response<Trip>>> GetRideStatus(int tripId)
        {
            Trip rideRequest = null;//await tripService.FindByIdAsync(tripId);

            if (rideRequest == null)
                return NotFound($"Ride request {tripId} not found.");

            return Ok($"Ride request {tripId} status: {rideRequest.Status}");
        }
    }
}