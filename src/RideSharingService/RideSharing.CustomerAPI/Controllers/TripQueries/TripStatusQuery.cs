using CSharpFunctionalExtensions;
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
        public async Task<ActionResult<Response<Trip>>> GetRideStatus(TripQueryDto queryDto)
        {
            Result<Trip> ride = await _mediator.Send(queryDto);

            if (ride.IsFailure)
                return NotFound($"Ride request {queryDto.TripId} not found.");

            return Ok($"Ride request {queryDto.TripId} status: {ride.Value.Status}");
        }
    }
}