namespace RideSharing.Application.TripUseCase.Commands.CustomerCancelTripCommand
{
	public record struct CustomerCancelTripCommandResponseDto(
		Guid CustomerId,
		Guid TripId,
		string Reason)
	{

	}
}
