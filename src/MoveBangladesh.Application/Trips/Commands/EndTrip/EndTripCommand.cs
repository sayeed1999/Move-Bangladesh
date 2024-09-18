using CSharpFunctionalExtensions;
using MediatR;

namespace MoveBangladesh.Application.Trips.Commands.EndTrip;

public record struct EndTripCommand(
	string DriverId,
	string TripId) : IRequest<Result<string>>
{
}
