using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripRequest.Commands.AcceptTripRequest;
using RideSharing.Common.Entities;

namespace RideSharing.CustomerAPI.Controllers.TripRequest.Commands
{
	[Route("api/external/trip-requests")]
	[ApiController]
	public class AcceptTripRequestCommand : ControllerBase
	{
		private readonly IMediator mediator;

		public AcceptTripRequestCommand(IMediator mediator)
		{
			this.mediator = mediator;
		}

		/// <summary>
		/// Use this endpoint to accept trip request for driver
		/// </summary>
		/// <param name="tripRequestId"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/accept-trip-request")]
		public async Task<ActionResult<Response<Guid>>> RequestRide(Guid tripRequestId)
		{
			var driverId = new Guid(); // TODO:- get customerId from httpContextAccessor!

			var model = new AcceptTripRequestDto(driverId, tripRequestId);

			var res = await mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}
	}
}
