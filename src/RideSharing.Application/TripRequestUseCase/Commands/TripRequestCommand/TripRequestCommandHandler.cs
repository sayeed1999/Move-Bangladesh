using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripRequestUseCase.Commands.TripRequestCommand
{
	public class TripRequestCommandHandler
		: IRequestHandler<TripRequestCommandDto, Result<TripRequestCommandResponseDto>>
	{
		private readonly ITripRequestRepository tripRequestRepository;
		private readonly ITripRequestLogRepository tripRequestLogRepository;
		private readonly ITripRepository tripRepository;
		private readonly ICustomerRepository customerRepository;

		public TripRequestCommandHandler(
			ITripRequestRepository tripRequestRepository,
			ITripRequestLogRepository tripRequestLogRepository,
			ITripRepository tripRepository,
			ICustomerRepository customerRepository)
		{
			this.tripRequestRepository = tripRequestRepository;
			this.tripRequestLogRepository = tripRequestLogRepository;
			this.tripRepository = tripRepository;
			this.customerRepository = customerRepository;
		}

		public async Task<Result<TripRequestCommandResponseDto>> Handle(TripRequestCommandDto model, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await customerRepository.FindByIdAsync(model.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<TripRequestCommandResponseDto>("Customer is not found.");
			}

			// Step 2: check customer has ongoing trip requests
			var requestedTrip = await tripRequestRepository.GetActiveTripRequestForCustomer(model.CustomerId);

			if (requestedTrip != null)
			{
				return Result.Failure<TripRequestCommandResponseDto>("Customer has already a requested trip.");
			}

			// Step 3: check customer has ongoing trips
			var unfinishedTrip = await tripRepository.GetActiveTripForCustomer(model.CustomerId);

			if (unfinishedTrip != null)
			{
				return Result.Failure<TripRequestCommandResponseDto>("Customer has already an ongoing trip.");
			}

			// Step 4: create trip request entity
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

			// Step 5: perform db operations

			TripRequest? res;

			var transaction = await tripRequestRepository.BeginTransactionAsync();
			try
			{
				res = await tripRequestRepository.AddAsync(tripRequest.Value);

				await tripRequestLogRepository.AddAsync(new TripRequestLog(res));

				await tripRequestRepository.CommitTransactionAsync(transaction);

				// Step 5: return response
				var responseDto = new TripRequestCommandResponseDto(res);

				return Result.Success(responseDto);
			}
			catch (Exception ex)
			{
				await tripRequestRepository.RollBackTransactionAsync(transaction);

				return Result.Failure<TripRequestCommandResponseDto>($"Failed with error: {ex.Message}");
			}
		}
	}
}
