using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripUseCase.Queries.TripStatusQuery
{
	public class TripStatusQueryDto : IRequest<Result<TripStatusQueryResponseDto>>
	{
		private TripStatusQueryDto() { }

		public static TripStatusQueryDto Create(Guid tripId)
		{
			return new TripStatusQueryDto()
			{
				TripId = tripId,
			};
		}

		public Guid TripId { get; private set; }
	}
}
