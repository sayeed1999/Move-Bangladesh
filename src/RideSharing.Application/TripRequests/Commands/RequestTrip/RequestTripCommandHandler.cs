using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripRequests.Commands.TripRequests
{
	public class RequestTripCommandHandler(
		IUnitOfWork unitOfWork,
		ITripRequestEventMessageBus messageBus)
		: IRequestHandler<RequestTripCommand, Result<string>>
	{
		public async Task<Result<string>> Handle(RequestTripCommand model, CancellationToken cancellationToken)
		{
			// Step 1: check customer exists
			var customerInDB = await unitOfWork.CustomerRepository.FindByIdAsync(model.CustomerId);

			if (customerInDB == null)
			{
				return Result.Failure<string>("Customer is not found.");
			}

			// Step 2: check customer has ongoing trip requests
			var requestedTrip = await unitOfWork.TripRequestRepository.GetActiveTripRequestForCustomer(model.CustomerId);

			if (requestedTrip != null)
			{
				return Result.Failure<string>("Customer has already a requested trip.");
			}

			// Step 3: check customer has ongoing trips
			var unfinishedTrip = await unitOfWork.TripRepository.GetActiveTripForCustomer(model.CustomerId);

			if (unfinishedTrip != null)
			{
				return Result.Failure<string>("Customer has already an ongoing trip.");
			}

			// Step 4: create trip request entity
			var tripRequest = new TripRequest
			{
				CustomerId = model.CustomerId,
				SourceX = model.Source.Item1,
				SourceY = model.Source.Item2,
				DestinationX = model.Destination.Item1,
				DestinationY = model.Destination.Item2,
				CabType = model.CabType,
				PaymentMethod = model.PaymentMethod,
			};

			// TODO: create fluent validator for the entity!!

			// Step 5: perform db operations
			try
			{
				await unitOfWork.TripRequestRepository.CreateAsync(tripRequest);

				var result = await unitOfWork.SaveChangesAsync();

				if (result.IsFailure)
				{
					return Result.Failure<string>(result.Error);
				}

				var messageDto = tripRequest.GetTripRequestDto();

				// Note: intentionally not awaited
				messageBus.PublishAsync(messageDto);

				return Result.Success(tripRequest.Id);
			}
			catch (Exception ex)
			{
				return Result.Failure<string>($"Failed with error: {ex.Message}");
			}
		}
	}
}
