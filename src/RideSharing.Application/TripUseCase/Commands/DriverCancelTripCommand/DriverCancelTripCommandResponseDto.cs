namespace RideSharing.Application.TripUseCase.Commands.DriverCancelTripCommand
{
	public record struct DriverCancelTripCommandResponseDto(
		Guid DriverId,
		Guid TripId,
		string Reason)
	{

	}
}
