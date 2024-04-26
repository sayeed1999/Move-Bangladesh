using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;

namespace RideSharing.Application.TripRequest.Commands.CancelTripRequest
{
	public class CancelTripRequestCommandHandler(
		ITripRequestRepository tripRequestRepository,
		ICustomerRepository customerRepository,
		ITripRequestEventMessageBus messageBus)
		: IRequestHandler<CancelTripRequestCommandDto, Result<Guid>>
	{
		public async Task<Result<Guid>> Handle(CancelTripRequestCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await customerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<Guid>("Customer is not found.");
			}

			// Step 2: check trip request exists
			var requestedTrip = await tripRequestRepository.GetActiveTripRequestForCustomer(request.CustomerId);

			if (requestedTrip == null)
			{
				return Result.Failure<Guid>("Customer has no pending requested trip.");
			}

			// Step 3: prepare domain entity
			Result entityResult = requestedTrip.Cancel();

			// Step 4: perform database operations
			try
			{
				// Note: log table is inserted from database triggers, not api

				var res = await tripRequestRepository.UpdateAsync(requestedTrip);

				messageBus.PublishAsync(requestedTrip.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(request.TripRequestId);
			}
			catch (Exception ex)
			{
				return Result.Failure<Guid>($"Failed with error: {ex.Message}");
			}
		}
	}
}
