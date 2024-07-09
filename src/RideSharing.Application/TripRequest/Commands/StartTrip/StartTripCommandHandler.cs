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
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus tripHandlerEventBus,
		IRideSharingProcessor rideSharingProcessor)
		: IRequestHandler<StartTripCommandDto, Result<long>>
	{
		public async Task<Result<long>> Handle(StartTripCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check valid trip request exists
			var tripRequestInDB = await unitOfWork.TripRequestRepository.FindByIdAsync(request.TripRequestId);

			if (tripRequestInDB == null)
			{
				return Result.Failure<long>("Trip Request is not found.");
			}

			// Step 2: check driver exists
			var driverInDB = await unitOfWork.DriverRepository.FindByIdAsync(request.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<long>("Driver is not found.");
			}

			// Step 3: check driver has ongoing trips
			var trip = await unitOfWork.TripRepository.GetActiveTripForDriver(request.DriverId);

			if (trip != null)
			{
				return Result.Failure<long>("Driver has an ongoing trip.");
			}

			// ** Security check !
			if (tripRequestInDB.Id != request.TripRequestId)
			{
				return Result.Failure<long>("Active trip request for driver does not match !!");
			}

			// Step 4: prepare entity
			var transitionValid = await rideSharingProcessor.IsTripRequestTransitionValid(tripRequestInDB.Status, TripRequestStatus.TRIP_STARTED);

			if (!transitionValid)
			{
				return Result.Failure<long>("TripRequest Status cannot be changed to desired status.");
			}

			tripRequestInDB.Modify(TripRequestStatus.TRIP_STARTED);

			// Step 5: create trip entity

			var newTripResult = TripFactory.Create(tripRequestInDB, request.DriverId);

			// Step 4: perform database operations

			try
			{
				// Note: log table is inserted from database triggers, not api

				unitOfWork.TripRequestRepository.Update(tripRequestInDB);

				await unitOfWork.TripRepository.CreateAsync(newTripResult.Value);

				// call UoW to save the changes in db.
				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<long>(result.Error);
				}

				tripHandlerEventBus.PublishAsync(tripRequestInDB.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(newTripResult.Value.Id);
			}
			catch (Exception ex)
			{
				return Result.Failure<long>($"Failed with error: {ex.Message}");
			}
		}
	}
}
