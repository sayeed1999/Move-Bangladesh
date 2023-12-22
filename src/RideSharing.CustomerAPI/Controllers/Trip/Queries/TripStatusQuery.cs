using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripUseCase.Queries.TripStatusQuery;
using RideSharing.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace RideSharing.CustomerAPI.Controllers.Trip.Queries
{
    [Route("api/external/trips")]
    [ApiController]
    public class TripStatusQuery : ControllerBase
    {
        private readonly IMediator _mediator;

        public TripStatusQuery(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tripId}/status")]
        public async Task<ActionResult<Response<TripStatusQueryResponseDto>>> GetRideStatus([Required] int tripId)
        {
            var dto = TripStatusQueryDto.Create(tripId);

            Result<TripStatusQueryResponseDto> ride = await _mediator.Send(dto);

            if (ride.IsFailure)
                return NotFound($"Ride request {dto.TripId} not found.");

            return Ok($"Ride request {dto.TripId} status: {ride.Value.Status}");
        }
    }
}