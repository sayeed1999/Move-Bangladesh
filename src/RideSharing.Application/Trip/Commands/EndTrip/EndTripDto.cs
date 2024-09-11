using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.Trip.Commands.EndTrip;

public record struct EndTripDto(
    string DriverId,
    string TripId) : IRequest<Result<string>>
{
}
