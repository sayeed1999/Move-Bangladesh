using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.TripRequest.Commands.CancelTripRequest
{
	public class CancelTripRequestCommandHandler(
		ITripRequestRepository tripRequestRepository,
		ICustomerRepository customerRepository,
		ITripRequestEventMessageBus messageBus,
		ITransitionChecker<TripRequestStatus> transitionChecker)
		: IRequestHandler<CancelTripRequestCommandDto, Result<long>>
	{
		public async Task<Result<long>> Handle(CancelTripRequestCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await customerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<long>("Customer is not found.");
			}

			// Step 2: check trip request exists
			var requestedTrip = await tripRequestRepository.GetActiveTripRequestForCustomer(request.CustomerId);

			if (requestedTrip == null)
			{
				return Result.Failure<long>("Customer has no pending requested trip.");
			}

			// Step 3: prepare domain entity
			var transitionValid = transitionChecker.IsTransitionValid(requestedTrip.Status, TripRequestStatus.CUSTOMER_CANCELED);

			if (!transitionValid)
			{
				return Result.Failure<long>("Trip Request Status cannot be changed to desired status.");
			}

			requestedTrip.Modify(TripRequestStatus.CUSTOMER_CANCELED);

			// Step 4: perform database operations
			try
			{
				// Note: log table is inserted from database triggers, not api

				var res = await tripRequestRepository.UpdateAsync(requestedTrip);

				messageBus.PublishAsync(requestedTrip.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(request.TripRequestId);
			}
			catch (Exception ex)
			{
				return Result.Failure<long>($"Failed with error: {ex.Message}");
			}
		}
	}
}
