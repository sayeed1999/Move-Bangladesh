using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Domain.Factories;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.TripRequest.Commands.StartTrip
{
	public class StartTripCommandHandler(
		ITripRequestRepository tripRequestRepository,
		ITripRepository tripRepository,
		IDriverRepository driverRepository,
		ITripRequestEventMessageBus tripHandlerEventBus,
		ITransitionChecker<TripRequestStatus> transitionChecker)
		: IRequestHandler<StartTripCommandDto, Result<long>>
	{
		public async Task<Result<long>> Handle(StartTripCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check valid trip request exists
			var tripRequestInDB = await tripRequestRepository.FindByIdAsync(request.TripRequestId);

			if (tripRequestInDB == null)
			{
				return Result.Failure<long>("Trip Request is not found.");
			}

			// Step 2: check driver exists
			var driverInDB = await driverRepository.FindByIdAsync(request.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<long>("Driver is not found.");
			}

			// Step 3: check driver has ongoing trips
			var trip = await tripRepository.GetActiveTripForDriver(request.DriverId);

			if (trip != null)
			{
				return Result.Failure<long>("Driver has an ongoing trip.");
			}

			// Step 4: prepare entity
			var transitionValid = transitionChecker.IsTransitionValid(tripRequestInDB.Status, TripRequestStatus.TRIP_STARTED);

			if (!transitionValid)
			{
				return Result.Failure<long>("TripRequest Status cannot be changed to desired status.");
			}

			tripRequestInDB.Modify(TripRequestStatus.TRIP_STARTED);

			// Step 5: create trip entity

			var newTripResult = TripFactory.Create(tripRequestInDB, request.DriverId);

			// Step 4: perform database operations

			var transaction = await tripRequestRepository.BeginTransactionAsync();

			try
			{
				// Note: log table is inserted from database triggers, not api

				await tripRequestRepository.UpdateAsync(tripRequestInDB);

				await tripRepository.AddAsync(newTripResult.Value);

				await transaction.CommitAsync();

				tripHandlerEventBus.PublishAsync(tripRequestInDB.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(newTripResult.Value.Id);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync(cancellationToken);

				return Result.Failure<long>($"Failed with error: {ex.Message}");
			}
		}
	}
}
