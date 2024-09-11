using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripRequest.Commands.StartTrip;

namespace RideSharing.CustomerAPI.Controllers.TripRequest.Commands
{
	[Route("api/external/trip-requests")]
	[ApiController]
	public class StartTripCommand : ControllerBase
	{
		private readonly IMediator mediator;

		public StartTripCommand(IMediator mediator)
		{
			this.mediator = mediator;
		}

		/// <summary>
		/// Use this endpoint to accept trip request for driver
		/// </summary>
		/// <param name="tripRequestId"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/start-trip")]
		public async Task<ActionResult> RequestRide(string tripRequestId)
		{
			var driverId = string.Empty; // TODO:- get customerId from httpContextAccessor!

			var model = new StartTripCommandDto(driverId, tripRequestId);

			var res = await mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}
	}
}
