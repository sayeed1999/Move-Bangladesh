using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripRequestUseCase.Commands.CancelTripRequestCommand
{
	public class CancelTripRequestCommandHandler
		: IRequestHandler<CancelTripRequestCommandDto, Result<CancelTripRequestCommandResponseDto>>
	{
		private readonly ITripRequestRepository tripRequestRepository;
		private readonly ICustomerRepository customerRepository;

		public CancelTripRequestCommandHandler(
			ITripRequestRepository tripRequestRepository,
			ICustomerRepository customerRepository)
		{
			this.tripRequestRepository = tripRequestRepository;
			this.customerRepository = customerRepository;
		}

		public async Task<Result<CancelTripRequestCommandResponseDto>> Handle(CancelTripRequestCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await customerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<CancelTripRequestCommandResponseDto>("Customer is not found.");
			}

			// Step 2: check trip request exists
			var requestedTrip = await tripRequestRepository.GetActiveTripRequestForCustomer(request.CustomerId);

			if (requestedTrip == null)
			{
				return Result.Failure<CancelTripRequestCommandResponseDto>("Customer has no pending requested trip.");
			}

			// Step 3: prepare domain entity
			Result<TripRequest> canceledTripRequest = TripRequest.Cancel(requestedTrip);

			// Step 4: perform database operations

			var transaction = await tripRequestRepository.BeginTransactionAsync();

			TripRequest res;

			try
			{
				// Note: log table is inserted from database triggers, not api

				res = await tripRequestRepository.UpdateAsync(canceledTripRequest.Value);

				await tripRequestRepository.CommitTransactionAsync(transaction);

				// Last Step: return result

				var responseDto = new CancelTripRequestCommandResponseDto(true);

				return Result.Success(responseDto);
			}
			catch (Exception ex)
			{
				await tripRequestRepository.RollBackTransactionAsync(transaction);

				return Result.Failure<CancelTripRequestCommandResponseDto>($"Failed with error: {ex.Message}");
			}
		}
	}
}
