using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.TripRequests.Commands.RejectByDriver
{
	public class RejectByDriverCommandHandler(
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus tripHandlerEventBus,
		IRideProcessingService rideProcessingService)
		: IRequestHandler<RejectByDriverCommandDto, Result<string>>
	{
		public async Task<Result<string>> Handle(RejectByDriverCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check driver exists
			var driverInDB = await unitOfWork.DriverRepository.FindByIdAsync(request.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<string>("Driver is not found.");
			}

			// Step 2: check trip request exists
			var activeTripRequest = await unitOfWork.TripRequestRepository.GetActiveTripRequestForDriver(request.DriverId);

			if (activeTripRequest == null)
			{
				return Result.Failure<string>("Driver has no active trip.");
			}

			// ** Security check !
			if (activeTripRequest.Id != request.TripRequestId)
			{
				return Result.Failure<string>("Active trip request for driver does not match !!");
			}

			// Step 3: prepare entity
			var transitionValid = await rideProcessingService.IsTripRequestTransitionValid(activeTripRequest.Status, TripRequestStatus.DRIVER_REJECTED_CUSTOMER);

			if (!transitionValid)
			{
				return Result.Failure<string>("TripRequest Status cannot be changed to desired status.");
			}

			activeTripRequest.Modify(TripRequestStatus.DRIVER_REJECTED_CUSTOMER);

			// Step 4: perform database operations

			try
			{
				// Note: log table is inserted from database triggers, not api

				unitOfWork.TripRequestRepository.Update(activeTripRequest);

				// call UoW to save the changes in db.
				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<string>(result.Error);
				}

				tripHandlerEventBus.PublishAsync(activeTripRequest.GetTripRequestDto());

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
