using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.TripRequest.Commands.RejectByDriver
{
	public class RejectByDriverCommandHandler(
		ITripRequestRepository tripRequestRepository,
		IDriverRepository driverRepository,
		ITripRequestEventMessageBus tripHandlerEventBus,
		ITransitionChecker<TripRequestStatus> transitionChecker)
		: IRequestHandler<RejectByDriverCommandDto, Result<long>>
	{
		public async Task<Result<long>> Handle(RejectByDriverCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check driver exists
			var driverInDB = await driverRepository.FindByIdAsync(request.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<long>("Driver is not found.");
			}

			// Step 2: check trip request exists
			var activeTripRequest = await tripRequestRepository.GetActiveTripRequestForDriver(request.DriverId);

			if (activeTripRequest == null)
			{
				return Result.Failure<long>("Driver has no active trip.");
			}

			// ** Security check !
			if (activeTripRequest.Id != request.TripRequestId)
			{
				return Result.Failure<long>("Active trip request for driver does not match !!");
			}

			// Step 3: prepare entity
			var transitionValid = transitionChecker.IsTransitionValid(activeTripRequest.Status, TripRequestStatus.DRIVER_REJECTED_CUSTOMER);

			if (!transitionValid)
			{
				return Result.Failure<long>("TripRequest Status cannot be changed to desired status.");
			}

			activeTripRequest.Modify(TripRequestStatus.DRIVER_REJECTED_CUSTOMER);

			// Step 4: perform database operations

			try
			{
				// Note: log table is inserted from database triggers, not api

				await tripRequestRepository.UpdateAsync(activeTripRequest);

				tripHandlerEventBus.PublishAsync(activeTripRequest.GetTripRequestDto());

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
