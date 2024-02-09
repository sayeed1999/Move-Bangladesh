using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripRequestUseCase.Commands.AcceptTripRequestCommand
{
	public class AcceptTripRequestHandler(
		IDriverRepository driverRepository,
		ITripRequestRepository tripRequestRepository,
		ITripRequestLogRepository tripRequestLogRepository,
		ITripRepository tripRepository,
		ITripLogRepository tripLogRepository
	)
		: IRequestHandler<AcceptTripRequestDto, Result<AcceptTripRequestResponseDto>>
	{
		public async Task<Result<AcceptTripRequestResponseDto>> Handle(AcceptTripRequestDto model, CancellationToken cancellationToken)
		{
			// Step 1: check valid trip request exists
			var tripRequestInDB = await tripRequestRepository.FindByIdAsync(model.TripRequestId);

			if (tripRequestInDB == null)
			{
				return Result.Failure<AcceptTripRequestResponseDto>("Trip Request is not found.");
			}

			// trip request is not valid if status is other than 'NoDriverAccepted'
			if (tripRequestInDB.Status != Domain.Enums.TripRequestStatus.NoDriverAccepted)
			{
				return Result.Failure<AcceptTripRequestResponseDto>("Trip Request is invalid.");
			}

			// trip request is invalid/expired if trip request is older than 1 minute
			var oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);
			if (tripRequestInDB.UpdatedAt < oneMinuteAgo)
			{
				return Result.Failure<AcceptTripRequestResponseDto>("Trip Request is expired.");
			}

			// Step 2: check driver exists
			var driverInDB = await driverRepository.FindByIdAsync(model.DriverId);

			if (driverInDB == null)
			{
				return Result.Failure<AcceptTripRequestResponseDto>("Driver is not found.");
			}

			// Step 3: check driver has ongoing trips
			var trip = await tripRepository.GetActiveTripForDriver(model.DriverId);

			if (trip != null)
			{
				return Result.Failure<AcceptTripRequestResponseDto>("Driver has an ongoing trip.");
			}

			// Step 4: create trip entity
			var tripRequest = TripRequest.DriverAccepted(tripRequestInDB);
			var newTrip = Trip.Create(tripRequestInDB, model.DriverId);

			// Step 5: perform db operations

			var transaction = await tripRequestRepository.BeginTransactionAsync();

			Trip res;

			try
			{
				// update trip request & insert log
				var tripRequestRes = await tripRequestRepository.UpdateAsync(tripRequest.Value);

				await tripRequestLogRepository.AddAsync(new TripRequestLog(tripRequestRes));

				// create trip & insert log
				res = await tripRepository.AddAsync(newTrip.Value);

				await tripLogRepository.AddAsync(new TripLog(res));

				// commit
				await tripRequestRepository.CommitTransactionAsync(transaction);

				// Last Step: return result

				var responseDto = new AcceptTripRequestResponseDto(res.DriverId, res.Id);

				return Result.Success(responseDto);
			}
			catch (Exception ex)
			{
				await tripRequestRepository.RollBackTransactionAsync(transaction);

				return Result.Failure<AcceptTripRequestResponseDto>($"Failed with error: {ex.Message}");
			}
		}
	}
}
