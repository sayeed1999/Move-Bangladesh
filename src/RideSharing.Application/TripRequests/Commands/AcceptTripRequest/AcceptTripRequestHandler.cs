using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.TripRequests.Commands.AcceptTripRequest
{
	public class AcceptTripRequestHandler(
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus tripRequestMessageBus,
		IRideProcessingService rideProcessingService)
		: IRequestHandler<AcceptTripRequestCommand, Result<string>>
	{
		public async Task<Result<string>> Handle(AcceptTripRequestCommand model, CancellationToken cancellationToken)
		{
			// Step 1: check valid trip request exists
			var tripRequestInDB = await unitOfWork.TripRequestRepository.FindByIdAsync(model.TripRequestId);

			if (tripRequestInDB == null)
			{
				return Result.Failure<string>("Trip Request is not found.");
			}

			// trip request is not valid if status is other than 'NoDriverAccepted'
			if (tripRequestInDB.Status != TripRequestStatus.NO_DRIVER_FOUND)
			{
				return Result.Failure<string>("Trip Request is invalid.");
			}

			// trip request is invalid/expired if trip request is older than 1 minute
			var oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);
			if (tripRequestInDB.LastModifiedAt < oneMinuteAgo)
			{
				return Result.Failure<string>("Trip Request is expired.");
			}

			// Step 2: check driver exists
			var driverInDB = await unitOfWork.DriverRepository.FindByIdAsync(model.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<string>("Driver is not found.");
			}

			// Step 3: check driver has ongoing trip requests
			var tripRequest = await unitOfWork.TripRequestRepository.GetActiveTripRequestForDriver(model.DriverId);

			if (tripRequest != null)
			{
				return Result.Failure<string>("Driver has an ongoing trip request.");
			}

			// Step 4: check driver has ongoing trips
			var trip = await unitOfWork.TripRepository.GetActiveTripForDriver(model.DriverId);

			if (trip != null)
			{
				return Result.Failure<string>("Driver has an ongoing trip.");
			}

			// Step 4: create trip entity
			bool transitionValid = await rideProcessingService.IsTripRequestTransitionValid(tripRequestInDB.Status, TripRequestStatus.DRIVER_ACCEPTED);

			if (!transitionValid)
			{
				return Result.Failure<string>("Trip Request Status cannot be changed to desired status.");
			}

			tripRequestInDB.Status = TripRequestStatus.DRIVER_ACCEPTED;
			tripRequestInDB.DriverId = model.DriverId;

			// Step 5: perform db operations
			try
			{
				unitOfWork.TripRequestRepository.Update(tripRequestInDB);

				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<string>(result.Error);
				}

				tripRequestMessageBus.PublishAsync(tripRequestInDB.GetTripRequestDto());

				return Result.Success(model.TripRequestId);
			}
			catch (Exception ex)
			{
				return Result.Failure<string>($"Failed with error: {ex.Message}");
			}
		}
	}
}
