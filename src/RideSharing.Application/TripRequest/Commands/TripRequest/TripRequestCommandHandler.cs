using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Domain.Factories;

namespace RideSharing.Application.TripRequest.Commands.TripRequest
{
	public class TripRequestCommandHandler(
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus messageBus)
		: IRequestHandler<TripRequestCommandDto, Result<long>>
	{
		public async Task<Result<long>> Handle(TripRequestCommandDto model, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await unitOfWork.CustomerRepository.FindByIdAsync(model.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<long>("Customer is not found.");
			}

			// Step 2: check customer has ongoing trip requests
			var requestedTrip = await unitOfWork.TripRequestRepository.GetActiveTripRequestForCustomer(model.CustomerId);

			if (requestedTrip != null)
			{
				return Result.Failure<long>("Customer has already a requested trip.");
			}

			// Step 3: check customer has ongoing trips
			var unfinishedTrip = await unitOfWork.TripRepository.GetActiveTripForCustomer(model.CustomerId);

			if (unfinishedTrip != null)
			{
				return Result.Failure<long>("Customer has already an ongoing trip.");
			}

			// Step 4: create trip request entity
			Result<TripRequestEntity> tripRequest = TripRequestFactory.Create(
				model.CustomerId,
				model.Source,
				model.Destination,
				model.CabType,
				model.PaymentMethod);

			if (tripRequest.IsFailure)
			{
				return Result.Failure<long>("Please provide valid data.");
			}

			// Step 5: perform db operations
			try
			{
				// Note: log table is inserted from database triggers, not api

				await unitOfWork.TripRequestRepository.CreateAsync(tripRequest.Value);

				// call UoW to save the changes in db.
				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<long>(result.Error);
				}

				// Note: this method call is not intentionally awaited!
				var messageDto = tripRequest.Value.GetTripRequestDto();

				messageBus.PublishAsync(messageDto);

				// Step 5: return response

				return Result.Success(tripRequest.Value.Id);
			}
			catch (Exception ex)
			{
				return Result.Failure<long>($"Failed with error: {ex.Message}");
			}
		}
	}
}
