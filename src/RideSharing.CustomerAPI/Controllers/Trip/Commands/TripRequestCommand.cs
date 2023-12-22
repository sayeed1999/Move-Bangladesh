using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripUseCase.Commands.TripRequestCommand;
using RideSharing.Common.Entities;

namespace RideSharing.CustomerAPI.Controllers.Trip.Commands
{
    [Route("api/external/trips")]
    [ApiController]
    public class TripRequestCommand : ControllerBase
    {
        private readonly IMediator _mediator;

        public TripRequestCommand(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("request")]
        public async Task<ActionResult<Response<TripRequestCommandResponseDto>>> RequestRide(TripRequestCommandDto model)
        {
            var res = await _mediator.Send(model);
            if (res.IsFailure) return BadRequest(res.Error);
            return Ok($"Ride request {res.Value.Id} submitted successfully.");
        }
    }
}