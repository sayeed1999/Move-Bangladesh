using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.Trips.Commands.EndTrip;

public record struct EndTripDto(
    string DriverId,
    string TripId) : IRequest<Result<string>>
{
}
