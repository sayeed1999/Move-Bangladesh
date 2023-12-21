using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Common.Entities;
using RideSharing.Domain;
using RideSharing.Domain.Dtos;

namespace RideSharing.CustomerAPI.Controllers.TripCommands
{
    [Route("api/external/trips")]
    [ApiController]
    public class TripRequestCommand : ControllerBase
    {
        private readonly IMediator _mediator;

        public TripRequestCommand(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("request")]
        public async Task<ActionResult<Response<Trip>>> RequestRide(TripRequestDto model)
        {
            var res = await _mediator.Send(model);
            if (res.IsFailure) return BadRequest(res.Error);
            return Ok($"Ride request {res.Value.Id} submitted successfully.");
        }
    }
}