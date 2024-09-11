using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripRequest.Commands.CancelTripRequest;

namespace RideSharing.CustomerAPI.Controllers.TripRequest.Commands
{

	[Route("api/external/trip-requests")]
	[ApiController]
	public class CancelTripRequestCommand : ControllerBase
	{
		private readonly IMediator _mediator;

		public CancelTripRequestCommand(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		/// Customer will cancel a trip using this endpoint
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/cancel-by-customer")]
		public async Task<ActionResult> RequestRide(string tripRequestId)
		{
			var customerId = string.Empty; // TODO:- get customerId from httpContextAccessor!

			var model = new CancelTripRequestCommandDto(customerId, tripRequestId);

			var res = await _mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}

	}
}
