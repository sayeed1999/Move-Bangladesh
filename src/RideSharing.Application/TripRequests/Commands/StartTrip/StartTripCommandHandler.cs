using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripRequests.Commands.StartTrip
{
	public class StartTripCommandHandler(
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus tripHandlerEventBus,
		IRideProcessingService rideProcessingService)
		: IRequestHandler<StartTripCommandDto, Result<string>>
	{
		public async Task<Result<string>> Handle(StartTripCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check valid trip request exists
			var tripRequestInDB = await unitOfWork.TripRequestRepository.FindByIdAsync(request.TripRequestId);

			if (tripRequestInDB == null)
			{
				return Result.Failure<string>("Trip Request is not found.");
			}

			// Step 2: check driver exists
			var driverInDB = await unitOfWork.DriverRepository.FindByIdAsync(request.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<string>("Driver is not found.");
			}

			// Step 3: check driver has ongoing trips
			var trip = await unitOfWork.TripRepository.GetActiveTripForDriver(request.DriverId);

			if (trip != null)
			{
				return Result.Failure<string>("Driver has an ongoing trip.");
			}

			// ** Security check !
			if (tripRequestInDB.Id != request.TripRequestId)
			{
				return Result.Failure<string>("Active trip request for driver does not match !!");
			}

			// Step 4: prepare entity
			var transitionValid = await rideProcessingService.IsTripRequestTransitionValid(tripRequestInDB.Status, TripRequestStatus.TRIP_STARTED);

			if (!transitionValid)
			{
				return Result.Failure<string>("TripRequest Status cannot be changed to desired status.");
			}

			tripRequestInDB.Status = TripRequestStatus.TRIP_STARTED;

			// Step 5: create trip entity

			var newTripResult = new Trip(tripRequestInDB, request.DriverId);

			// Step 4: perform database operations

			try
			{
				// Note: log table is inserted from database triggers, not api

				unitOfWork.TripRequestRepository.Update(tripRequestInDB);

				await unitOfWork.TripRepository.CreateAsync(newTripResult);

				// call UoW to save the changes in db.
				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<string>(result.Error);
				}

				tripHandlerEventBus.PublishAsync(tripRequestInDB.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(newTripResult.Id);
			}
			catch (Exception ex)
			{
				return Result.Failure<string>($"Failed with error: {ex.Message}");
			}
		}
	}
}
