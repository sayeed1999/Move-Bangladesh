using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.TripRequests.Commands.CancelTripRequest
{
	public class CancelRequestTripCommandHandler(
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus messageBus,
		IRideProcessingService rideProcessingService)
		: IRequestHandler<CancelRequestTripCommandDto, Result<string>>
	{
		public async Task<Result<string>> Handle(CancelRequestTripCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await unitOfWork.CustomerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<string>("Customer is not found.");
			}

			// Step 2: check trip request exists
			var requestedTrip = await unitOfWork.TripRequestRepository.GetActiveTripRequestForCustomer(request.CustomerId);

			if (requestedTrip == null)
			{
				return Result.Failure<string>("Customer has no pending requested trip.");
			}

			// ** Security check !
			if (requestedTrip.Id != request.TripRequestId)
			{
				return Result.Failure<string>("Active trip request for customer does not match !!");
			}

			// Step 3: prepare domain entity
			var transitionValid = await rideProcessingService.IsTripRequestTransitionValid(requestedTrip.Status, TripRequestStatus.CUSTOMER_CANCELED);

			if (!transitionValid)
			{
				return Result.Failure<string>("Trip Request Status cannot be changed to desired status.");
			}

			requestedTrip.Modify(TripRequestStatus.CUSTOMER_CANCELED);

			// Step 4: perform database operations
			try
			{
				// Note: log table is inserted from database triggers, not api

				unitOfWork.TripRequestRepository.Update(requestedTrip);

				// call UoW to save the changes in db.
				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<string>(result.Error);
				}

				messageBus.PublishAsync(requestedTrip.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(request.TripRequestId);
			}
			catch (Exception ex)
			{
				return Result.Failure<string>($"Failed with error: {ex.Message}");
			}
		}
	}
}
