namespace RideSharing.Application.TripRequestUseCase.Commands.AcceptTripRequestCommand
{
	public record struct AcceptTripRequestResponseDto(
		Guid DrverId,
		Guid TripId,
		bool Accepted = true)
	{
	}
}
