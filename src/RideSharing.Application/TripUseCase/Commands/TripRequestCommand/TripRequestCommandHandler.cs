using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Domain.Enums;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
	public class TripRequestCommandHandler
		: IRequestHandler<TripRequestCommandDto, Result<TripRequestCommandResponseDto>>
	{
		private readonly ITripRequestRepository tripRequestRepository;
		private readonly ITripRepository tripRepository;
		private readonly ICustomerRepository customerRepository;

		public TripRequestCommandHandler(
			ITripRequestRepository tripRequestRepository,
			ITripRepository tripRepository,
			ICustomerRepository customerRepository)
		{
			this.tripRequestRepository = tripRequestRepository;
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

			// Step 2: check customer has ongoing trip requests
			// If a trip is requested in less than one minute and it is neither canceled nor started, it is considered an active requested trip.
			// If a trip request has no activity within one minute, it is considered auto-canceled.
			DateTime oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);

			var requestedTrip = await this.tripRequestRepository.DbSet.FirstOrDefaultAsync(
				x => x.Status == TripRequestStatus.NoDriverAccepted
					&& x.UpdatedAt >= oneMinuteAgo);

			if (requestedTrip != null)
			{
				return Result.Failure<TripRequestCommandResponseDto>("Customer has already a requested trip.");
			}

			// Step 3: check customer has ongoing trips
			var unfinishedTrip = await this.tripRepository.DbSet.FirstOrDefaultAsync(
				x => x.TripStatus != TripStatus.TripCompleted);

			if (unfinishedTrip != null)
			{
				return Result.Failure<TripRequestCommandResponseDto>("Customer has already an ongoing trip.");
			}

			// Step 4: create ride entity
			Result<TripRequest> tripRequest = TripRequest.Create(
				model.CustomerId,
				model.Source,
				model.Destination,
				model.CabType,
				model.PaymentMethod);

			if (tripRequest.IsFailure)
			{
				return Result.Failure<TripRequestCommandResponseDto>("Please provide valid data.");
			}

			// Step 5: save in db
			var res = await this.tripRequestRepository.AddAsync(tripRequest.Value);

			// Step 5: return response
			var responseDto = new TripRequestCommandResponseDto(res);

			return Result.Success<TripRequestCommandResponseDto>(responseDto);
		}
	}
}
