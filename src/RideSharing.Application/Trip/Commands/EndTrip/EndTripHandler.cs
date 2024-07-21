using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.Trip.Commands.EndTrip;

public class EndTripHandler(
    IUnitOfWork unitOfWork,
    ITripEventMessageBus tripMessageBus,
    IRideProcessingService rideProcessingService
)
    : IRequestHandler<EndTripDto, Result<long>>
{
    public async Task<Result<long>> Handle(EndTripDto model, CancellationToken cancellationToken)
    {
        var tripInDB = await unitOfWork.TripRepository.HasOngoingTrip(
            model.TripId, 
            model.DriverId);

        if (tripInDB == null)
        {
            return Result.Failure<long>("Ongoing Trip is not found.");
        }

        bool transitionValid = await rideProcessingService.IsTripTransitionValid(tripInDB.TripStatus, TripStatus.WAITING_FOR_PAYMENT);

        if (!transitionValid)
        {
            return Result.Failure<long>("Trip Status cannot be changed to desired status.");
        }

        tripInDB.Modify(TripStatus.WAITING_FOR_PAYMENT);

        try
        {
            unitOfWork.TripRepository.Update(tripInDB);

            var result = await unitOfWork.SaveChangesAsync();

            if (result.IsFailure)
            {
                return Result.Failure<long>(result.Error);
            }

            tripMessageBus.PublishAsync(tripInDB.GetTripDto());

            return Result.Success(model.TripId);
        }
        catch (Exception ex)
        {
            return Result.Failure<long>($"Failed with error: {ex.Message}");
        }
    }
}
