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
		: IRequestHandler<RejectByDriverCommandDto, Result<Guid>>
	{
		public async Task<Result<Guid>> Handle(RejectByDriverCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check driver exists
			var driverInDB = await driverRepository.FindByIdAsync(request.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<Guid>("Driver is not found.");
			}

			// Step 2: check trip request exists
			var activeTripRequest = await tripRequestRepository.GetActiveTripRequestForDriver(request.DriverId);

			if (activeTripRequest == null)
			{
				return Result.Failure<Guid>("Driver has no active trip.");
			}

			// Step 3: prepare entity
			var transitionValid = transitionChecker.IsTransitionValid(activeTripRequest.Status, TripRequestStatus.DRIVER_REJECTED_CUSTOMER);

			if (!transitionValid)
			{
				return Result.Failure<Guid>("TripRequest Status cannot be changed to desired status.");
			}

			activeTripRequest.Modify(TripRequestStatus.DRIVER_REJECTED_CUSTOMER);

			// Step 4: perform database operations

			try
			{
				// Note: log table is inserted from database triggers, not api

				await tripRequestRepository.UpdateAsync(activeTripRequest);

				tripHandlerEventBus.PublishAsync(activeTripRequest.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(request.TripId);
			}
			catch (Exception ex)
			{
				return Result.Failure<Guid>($"Failed with error: {ex.Message}");
			}
		}
	}
}
