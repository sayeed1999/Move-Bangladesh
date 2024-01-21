using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
	public class TripRequestCommandHandler
		: IRequestHandler<TripRequestCommandDto, Result<TripRequestCommandResponseDto>>
	{
		private readonly ITripRepository tripRepository;
		private readonly ICustomerRepository customerRepository;

		public TripRequestCommandHandler(ITripRepository tripRepository,
			ICustomerRepository customerRepository)
		{
			this.tripRepository = tripRepository;
			this.customerRepository = customerRepository;
		}

		public async Task<Result<TripRequestCommandResponseDto>> Handle(TripRequestCommandDto model, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await this.customerRepository.FindByIdAsync(model.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<TripRequestCommandResponseDto>("Customer is not found.");
			}

			// Step 2: check customer has pending rides
			var pendingTrip = await this.tripRepository.DbSet.FirstOrDefaultAsync(x => x.IsActive == true);

			if (pendingTrip != null)
			{
				return Result.Failure<TripRequestCommandResponseDto>("Customer has already an unfinished trip.");
			}

			// Step 3: create ride entity
			Result<Trip> trip = Trip.RequestTrip(
				model.CustomerId,
				model.Source,
				model.Destination,
				model.CabType);

			if (trip.IsFailure)
			{
				return Result.Failure<TripRequestCommandResponseDto>("Please provide valid data.");
			}

			// Step 4: save in db
			var res = await this.tripRepository.AddAsync(trip.Value);

			// Step 5: return response
			var responseDto = new TripRequestCommandResponseDto(res);

			return Result.Success<TripRequestCommandResponseDto>(responseDto);
		}
	}
}
