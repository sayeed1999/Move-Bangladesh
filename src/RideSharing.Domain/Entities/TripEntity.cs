using CSharpFunctionalExtensions;
using NetTopologySuite.Geometries;
using RideSharing.Common.MessageQueues.Messages;

namespace RideSharing.Domain.Entities;

public class TripEntity : BaseEntity
{
	public long TripRequestId { get; set; }
	public virtual TripRequestEntity TripRequest { get; set; }
	public long CustomerId { get; set; }
	public virtual CustomerEntity Customer { get; set; }
	public long DriverId { get; set; }
	public virtual DriverEntity Driver { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public TripStatus TripStatus { get; set; }
	public Point Source { get; set; }
	public Point Destination { get; set; }
	public CabType CabType { get; set; }
	public virtual ICollection<PaymentEntity> Payments { get; set; }
	public virtual ICollection<TripLogEntity> TripLogs { get; set; }

	public Result Modify(TripStatus status)
	{
		TripStatus = status;

		return Result.Success();
	}


	public TripDto GetTripDto()
	{
		return new TripDto(
			Id,
			TripRequestId,
			CustomerId,
			DriverId,
			nameof(PaymentMethod),
			nameof(TripStatus),
			Source.ToText(),
			Destination.ToText(),
			nameof(CabType));
	}
}

public enum TripStatus
{
	ONGOING = 1,
	WAITING_FOR_PAYMENT = 2,
	PAYMENT_COMPLETED = 3,
}
