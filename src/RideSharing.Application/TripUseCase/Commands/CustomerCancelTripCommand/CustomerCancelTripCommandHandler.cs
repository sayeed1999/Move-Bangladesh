using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Commands.CustomerCancelTripCommand
{
	public class CustomerCancelTripCommandHandler(
		ITripRepository tripRepository,
		ICustomerRepository customerRepository)
		: IRequestHandler<CustomerCancelTripCommandDto, Result<CustomerCancelTripCommandResponseDto>>
	{
		public async Task<Result<CustomerCancelTripCommandResponseDto>> Handle(CustomerCancelTripCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await customerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<CustomerCancelTripCommandResponseDto>("Customer is not found.");
			}

			// Step 2: check trip request exists
			var activeTrip = await tripRepository.GetActiveTripForCustomer(request.CustomerId);

			if (activeTrip == null)
			{
				return Result.Failure<CustomerCancelTripCommandResponseDto>("Customer has no active trip.");
			}

			// Step 3: prepare entity
			var modifiedTripResult = Trip.CancelByCustomer(activeTrip);

			if (modifiedTripResult.IsFailure)
			{
				return Result.Failure<CustomerCancelTripCommandResponseDto>(modifiedTripResult.Error);
			}

			var modifiedTrip = modifiedTripResult.Value;

			// Step 4: perform database operations

			var transaction = await tripRepository.BeginTransactionAsync();

			Trip res;

			try
			{
				// Note: log table is inserted from database triggers, not api

				res = await tripRepository.UpdateAsync(modifiedTrip);

				await tripRepository.CommitTransactionAsync(transaction);

				// Last Step: return result

				var responseDto = new CustomerCancelTripCommandResponseDto(request.CustomerId, request.TripId, request.Reason);

				return Result.Success(responseDto);
			}
			catch (Exception ex)
			{
				await tripRepository.RollBackTransactionAsync(transaction);

				return Result.Failure<CustomerCancelTripCommandResponseDto>($"Failed with error: {ex.Message}");
			}
		}
	}
}
