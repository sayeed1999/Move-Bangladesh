using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Common.Entities;
using RideSharing.Domain;
using RideSharing.Domain.Dtos;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult<Response<Trip>>> GetRideStatus([Required] int tripId)
        {
            var dto = TripQueryDto.Create(tripId);

            Result<Trip> ride = await _mediator.Send(dto);

            if (ride.IsFailure)
                return NotFound($"Ride request {dto.TripId} not found.");

            return Ok($"Ride request {dto.TripId} status: {ride.Value.Status}");
        }
    }
}