using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.Trip.Commands.CustomerCancelTrip
{
	public class RejectByCustomerCommandHandler(
		ITripRequestRepository tripRequestRepository,
		ICustomerRepository customerRepository,
		ITripRequestEventMessageBus messageBus,
		ITransitionChecker<TripRequestStatus> transitionChecker)
		: IRequestHandler<RejectByCustomerCommandDto, Result<Guid>>
	{
		public async Task<Result<Guid>> Handle(RejectByCustomerCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await customerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<Guid>("Customer is not found.");
			}

			// Step 2: check trip request exists
			var activeTripRequest = await tripRequestRepository.GetActiveTripRequestForCustomer(request.CustomerId);

			if (activeTripRequest == null)
			{
				return Result.Failure<Guid>("Customer has no active trip request.");
			}

			// Step 3: Check transition valid or not
			var transitionValid = transitionChecker.IsTransitionValid(activeTripRequest.Status, TripRequestStatus.CUSTOMER_REJECTED_DRIVER);

			if (!transitionValid)
			{
				return Result.Failure<Guid>("TripRequest Status cannot be changed to desired status.");
			}

			// Step 4: prepare entity
			activeTripRequest.Modify(TripRequestStatus.CUSTOMER_REJECTED_DRIVER);

			// Step 5: perform database operations
			try
			{
				// Note: log table is inserted from database triggers, not api

				var res = await tripRequestRepository.UpdateAsync(activeTripRequest);

				messageBus.PublishAsync(activeTripRequest.GetTripRequestDto());

				// Last Step: return result

				return Result.Success(request.TripId);
			}
			catch (Exception ex)
			{
				return Result.Failure<Guid>($"Failed with error: {ex.Message}");
			}
		}
	}
}
