using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.Domain.Factories;

namespace RideSharing.Application.TripRequestUseCase.Commands.TripRequestCommand
{
	public class TripRequestCommandHandler(
		ITripRequestRepository tripRequestRepository,
		ITripRepository tripRepository,
		ICustomerRepository customerRepository,
		ITripRequestEventMessageBus messageBus)
		: IRequestHandler<TripRequestCommandDto, Result<TripRequestCommandResponseDto>>
	{
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
			Result<TripRequest> tripRequest = TripRequestFactory.Create(
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
			try
			{
				// Note: log table is inserted from database triggers, not api

				var res = await tripRequestRepository.AddAsync(tripRequest.Value);

				// Note: this method call is not intentionally awaited!
				var messageDto = TripRequest.GetTripRequestDto(tripRequest.Value);
				messageBus.PublishAsync(messageDto);

				// Step 5: return response
				var responseDto = new TripRequestCommandResponseDto(res);

				return Result.Success(responseDto);
			}
			catch (Exception ex)
			{
				return Result.Failure<TripRequestCommandResponseDto>($"Failed with error: {ex.Message}");
			}
		}
	}
}
