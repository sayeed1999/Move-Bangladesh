using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.Trip.Commands.StartTrip;
using RideSharing.Common.Entities;

namespace RideSharing.CustomerAPI.Controllers.Trip.Commands
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
		public async Task<ActionResult<Response<Guid>>> RequestRide(Guid tripRequestId)
		{
			var driverId = new Guid(); // TODO:- get customerId from httpContextAccessor!

			var model = new StartTripCommandDto(driverId, tripRequestId);

			var res = await mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}
	}
}
