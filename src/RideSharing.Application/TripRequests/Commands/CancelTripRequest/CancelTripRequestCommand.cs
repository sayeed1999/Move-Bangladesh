using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripRequests.Commands.CancelTripRequest
{
	public class CancelTripRequestCommand : IRequest<Result<string>>
	{
		public required string CustomerId { get; set; }
		public required string TripRequestId { get; set; }

		public class Handler(
			IUnitOfWork unitOfWork,
			ITripRequestEventMessageBus messageBus,
			IRideProcessingService rideProcessingService)
			: IRequestHandler<CancelTripRequestCommand, Result<string>>
		{
			public async Task<Result<string>> Handle(CancelTripRequestCommand request, CancellationToken cancellationToken)
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

				// Step 3: check transition validity by calling processor service
				var transitionValid = await rideProcessingService.IsTripRequestTransitionValid(requestedTrip.Status, TripRequestStatus.CUSTOMER_CANCELED);

				if (!transitionValid)
				{
					return Result.Failure<string>("Trip Request Status cannot be changed to desired status.");
				}

				requestedTrip.Status = TripRequestStatus.CUSTOMER_CANCELED;

				// Step 4: perform database operations
				try
				{
					unitOfWork.TripRequestRepository.Update(requestedTrip);

					var result = await unitOfWork.SaveChangesAsync();

					if (result.IsFailure)
					{
						return Result.Failure<string>(result.Error);
					}

					messageBus.PublishAsync(requestedTrip.GetTripRequestDto());

					return Result.Success(request.TripRequestId);
				}
				catch (Exception ex)
				{
					return Result.Failure<string>($"Failed with error: {ex.Message}");
				}
			}
		}
	}
}
