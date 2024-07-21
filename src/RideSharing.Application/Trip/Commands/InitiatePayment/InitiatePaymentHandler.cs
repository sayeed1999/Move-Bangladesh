using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Application.Trip.Commands.InitiatePayment;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.Trip.Commands.EndTrip;

public class InitiatePaymentHandler(
    IUnitOfWork unitOfWork,
    ITripEventMessageBus tripMessageBus,
    IRideProcessingService rideProcessingService
)
    : IRequestHandler<InitiatePaymentDto, Result<long>>
{
    public async Task<Result<long>> Handle(InitiatePaymentDto model, CancellationToken cancellationToken)
    {
        var tripInDB = await unitOfWork.TripRepository.HasTripWaitingForPayment(
            model.TripId, 
            model.CustomerId);

        if (tripInDB == null)
        {
            return Result.Failure<long>("Ongoing Trip is not found.");
        }

        // TODO: - call service for payment processing and return error if payment failed!

        bool transitionValid = await rideProcessingService.IsTripTransitionValid(tripInDB.TripStatus, TripStatus.PAYMENT_COMPLETED);

        if (!transitionValid)
        {
            return Result.Failure<long>("Trip Status cannot be changed to desired status.");
        }

        tripInDB.Modify(TripStatus.PAYMENT_COMPLETED);

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
