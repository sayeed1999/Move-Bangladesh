using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.Trips.Commands.EndTrip;

namespace RideSharing.CustomerAPI.Controllers.Trip.Commands
{
	[Route("api/external/trips")]
	[ApiController]
	public class EndTripCommand(
		IMediator mediator) : ControllerBase
	{
		/// <summary>
		/// Call this endpoint from driver's end when destination has been reached.
		/// </summary>
		/// <param name="tripId"></param>
		/// <returns></returns>
		[HttpPut("{tripId}/end-trip")]
		public async Task<ActionResult> Handle(string tripId)
		{
			var driverId = string.Empty; // TODO:- get driverId from httpContextAccessor!

			var model = new EndTripDto(driverId, tripId);

			var res = await mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}
	}
}
