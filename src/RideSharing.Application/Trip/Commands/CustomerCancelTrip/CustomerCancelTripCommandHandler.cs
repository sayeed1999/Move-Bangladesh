using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Application.Trip.Commands.CustomerCancelTrip
{
	public class CustomerCancelTripCommandHandler(
		ITripRepository tripRepository,
		ICustomerRepository customerRepository,
		ITripEventMessageBus messageBus,
		ITransitionChecker<TripStatus> transitionChecker)
		: IRequestHandler<CustomerCancelTripCommandDto, Result<Guid>>
	{
		public async Task<Result<Guid>> Handle(CustomerCancelTripCommandDto request, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await customerRepository.FindByIdAsync(request.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<Guid>("Customer is not found.");
			}

			// Step 2: check trip request exists
			var activeTrip = await tripRepository.GetActiveTripForCustomer(request.CustomerId);

			if (activeTrip == null)
			{
				return Result.Failure<Guid>("Customer has no active trip.");
			}

			// Step 3: Check transition valid or not
			var transitionValid = transitionChecker.IsTransitionValid(activeTrip.TripStatus, TripStatus.CustomerCanceled);

			if (!transitionValid)
			{
				return Result.Failure<Guid>("Trip Status cannot be changed to desired status.");
			}

			// Step 4: prepare entity
			activeTrip.Modify(TripStatus.CustomerCanceled);

			// Step 5: perform database operations
			try
			{
				// Note: log table is inserted from database triggers, not api

				var res = await tripRepository.UpdateAsync(activeTrip);

				messageBus.PublishAsync(activeTrip.GetTripDto());

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
