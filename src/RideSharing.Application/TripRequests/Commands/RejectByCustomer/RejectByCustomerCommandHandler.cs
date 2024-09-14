using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.TripRequests.Commands.RejectByCustomer
{
	public class RejectByCustomerCommandHandler(
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus messageBus,
		IRideProcessingService rideProcessingService)
		: IRequestHandler<RejectByCustomerCommand, Result<string>>
	{
		public async Task<Result<string>> Handle(RejectByCustomerCommand request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await unitOfWork.CustomerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<string>("Customer is not found.");
			}

			// Step 2: check trip request exists
			var activeTripRequest = await unitOfWork.TripRequestRepository.GetActiveTripRequestForCustomer(request.CustomerId);

			if (activeTripRequest == null)
			{
				return Result.Failure<string>("Customer has no active trip request.");
			}

			// ** Security check !
			if (activeTripRequest.Id != request.TripRequestId)
			{
				return Result.Failure<string>("Active trip request for customer does not match !!");
			}

			// Step 3: Check transition valid or not
			var transitionValid = await rideProcessingService.IsTripRequestTransitionValid(activeTripRequest.Status, TripRequestStatus.CUSTOMER_REJECTED_DRIVER);

			if (!transitionValid)
			{
				return Result.Failure<string>("TripRequest Status cannot be changed to desired status.");
			}

			// Step 4: prepare entity
			activeTripRequest.Status = TripRequestStatus.CUSTOMER_REJECTED_DRIVER;

			// Step 5: perform database operations
			try
			{
				unitOfWork.TripRequestRepository.Update(activeTripRequest);

				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<string>(result.Error);
				}

				messageBus.PublishAsync(activeTripRequest.GetTripRequestDto());

				return Result.Success(request.TripRequestId);
			}
			catch (Exception ex)
			{
				return Result.Failure<string>($"Failed with error: {ex.Message}");
			}
		}
	}
}
