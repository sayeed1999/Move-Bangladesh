using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.TripRequest.Commands.AcceptTripRequest
{
	public class AcceptTripRequestHandler(
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus tripRequestMessageBus,
		IRideSharingProcessor rideSharingProcessor
		// ITransitionChecker<TripRequestStatus> transitionChecker // old way
	)
		: IRequestHandler<AcceptTripRequestDto, Result<long>>
	{
		public async Task<Result<long>> Handle(AcceptTripRequestDto model, CancellationToken cancellationToken)
		{
			// Step 1: check valid trip request exists
			var tripRequestInDB = await unitOfWork.TripRequestRepository.FindByIdAsync(model.TripRequestId);

			if (tripRequestInDB == null)
			{
				return Result.Failure<long>("Trip Request is not found.");
			}

			// trip request is not valid if status is other than 'NoDriverAccepted'
			if (tripRequestInDB.Status != TripRequestStatus.NO_DRIVER_FOUND)
			{
				return Result.Failure<long>("Trip Request is invalid.");
			}

			// trip request is invalid/expired if trip request is older than 1 minute
			var oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);
			if (tripRequestInDB.LastModifiedAt < oneMinuteAgo)
			{
				return Result.Failure<long>("Trip Request is expired.");
			}

			// Step 2: check driver exists
			var driverInDB = await unitOfWork.DriverRepository.FindByIdAsync(model.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<long>("Driver is not found.");
			}

			// Step 3: check driver has ongoing trip requests
			var tripRequest = await unitOfWork.TripRequestRepository.GetActiveTripRequestForDriver(model.DriverId);

			if (tripRequest != null)
			{
				return Result.Failure<long>("Driver has an ongoing trip request.");
			}

			// Step 4: check driver has ongoing trips
			var trip = await unitOfWork.TripRepository.GetActiveTripForDriver(model.DriverId);

			if (trip != null)
			{
				return Result.Failure<long>("Driver has an ongoing trip.");
			}

			// Step 4: create trip entity
			bool transitionValid = await rideSharingProcessor.IsTripRequestTransitionValid(tripRequestInDB.Status, TripRequestStatus.DRIVER_ACCEPTED);
			// var transitionValid = transitionChecker.IsTransitionValid(tripRequestInDB.Status, TripRequestStatus.DRIVER_ACCEPTED); // old way

			if (!transitionValid)
			{
				return Result.Failure<long>("Trip Request Status cannot be changed to desired status.");
			}

			tripRequestInDB.Modify(TripRequestStatus.DRIVER_ACCEPTED, model.DriverId);

			// Step 5: perform db operations

			try
			{
				// Note: log table is inserted from database triggers, not api

				// update trip request
				unitOfWork.TripRequestRepository.Update(tripRequestInDB);

				// call UoW to save the changes in db.
				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<long>(result.Error);
				}

				tripRequestMessageBus.PublishAsync(tripRequestInDB.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(model.TripRequestId);
			}
			catch (Exception ex)
			{
				return Result.Failure<long>($"Failed with error: {ex.Message}");
			}
		}
	}
}
