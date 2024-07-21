using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.Trip.Commands.EndTrip;

public record struct EndTripDto(
    long DriverId,
    long TripId) : IRequest<Result<long>>
{
}
