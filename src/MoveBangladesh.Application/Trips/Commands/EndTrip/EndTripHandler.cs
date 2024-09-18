using CSharpFunctionalExtensions;
using MediatR;
using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Application.Trips.Commands.EndTrip;

public class EndTripHandler(
	IUnitOfWork unitOfWork,
	ITripEventMessageBus tripMessageBus,
	IRideProcessingService rideProcessingService
)
	: IRequestHandler<EndTripCommand, Result<string>>
{
	public async Task<Result<string>> Handle(EndTripCommand model, CancellationToken cancellationToken)
	{
		var tripInDB = await unitOfWork.TripRepository.GetActiveTripForDriver(
			model.DriverId);

		if (tripInDB == null)
		{
			return Result.Failure<string>("Ongoing Trip is not found.");
		}

		if (model.TripId != tripInDB.Id)
		{
			return Result.Failure<string>("TripId doesn't match.");
		}

		bool transitionValid = await rideProcessingService.IsTripTransitionValid(tripInDB.TripStatus, TripStatus.WAITING_FOR_PAYMENT);

		if (!transitionValid)
		{
			return Result.Failure<string>("Trip Status cannot be changed to desired status.");
		}

		tripInDB.Modify(TripStatus.WAITING_FOR_PAYMENT);

		try
		{
			unitOfWork.TripRepository.Update(tripInDB);

			var result = await unitOfWork.SaveChangesAsync();

			if (result.IsFailure)
			{
				return Result.Failure<string>(result.Error);
			}

			tripMessageBus.PublishAsync(tripInDB.GetTripDto());

			return Result.Success(model.TripId);
		}
		catch (Exception ex)
		{
			return Result.Failure<string>($"Failed with error: {ex.Message}");
		}
	}
}
