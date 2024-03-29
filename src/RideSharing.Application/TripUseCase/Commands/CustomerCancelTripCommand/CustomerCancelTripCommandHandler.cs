using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;

namespace RideSharing.Application.TripUseCase.Commands.CustomerCancelTripCommand
{
	public class CustomerCancelTripCommandHandler(
		ITripRepository tripRepository,
		ICustomerRepository customerRepository,
		ITripEventMessageBus messageBus)
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
			var entityResult = activeTrip.CancelByCustomer();

			if (entityResult.IsFailure)
			{
				return Result.Failure<CustomerCancelTripCommandResponseDto>(entityResult.Error);
			}

			// Step 4: perform database operations
			try
			{
				// Note: log table is inserted from database triggers, not api

				var res = await tripRepository.UpdateAsync(activeTrip);

				messageBus.PublishAsync(activeTrip.GetTripDto());

				// Last Step: return result
				var responseDto = new CustomerCancelTripCommandResponseDto(request.CustomerId, request.TripId, request.Reason);

				return Result.Success(responseDto);
			}
			catch (Exception ex)
			{
				return Result.Failure<CustomerCancelTripCommandResponseDto>($"Failed with error: {ex.Message}");
			}
		}
	}
}
