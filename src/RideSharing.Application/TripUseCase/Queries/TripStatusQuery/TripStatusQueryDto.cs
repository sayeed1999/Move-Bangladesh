using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripUseCase.Queries.TripStatusQuery
{
	public record struct TripStatusQueryDto(
		Guid TripId)
		: IRequest<Result<TripStatusQueryResponseDto>>
	{
		public static TripStatusQueryDto Create(Guid TripId)
		{
			return new TripStatusQueryDto(TripId);
		}
	}
}
