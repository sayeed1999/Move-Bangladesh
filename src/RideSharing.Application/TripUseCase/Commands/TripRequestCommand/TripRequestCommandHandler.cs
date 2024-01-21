using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
	public class TripRequestCommandHandler
		: IRequestHandler<TripRequestCommandDto, Result<TripRequestCommandResponseDto>>
	{
		private readonly ITripRepository tripRepository;

		public TripRequestCommandHandler(ITripRepository tripRepository)
		{
			this.tripRepository = tripRepository;
		}

		public async Task<Result<TripRequestCommandResponseDto>> Handle(TripRequestCommandDto model, CancellationToken cancellationToken)
		{
			Result<Trip> trip = Trip.RequestTrip(
				model.CustomerId,
				model.Source,
				model.Destination,
				model.CabType);

			if (trip.IsFailure) return Result.Failure<TripRequestCommandResponseDto>("Please provide valid data.");

			var res = await this.tripRepository.AddAsync(trip.Value);

			var responseDto = new TripRequestCommandResponseDto(res);

			return Result.Success<TripRequestCommandResponseDto>(responseDto);
		}
	}
}
