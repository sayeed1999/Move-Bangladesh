using NetTopologySuite.Geometries;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities
{
	public class TripLog : BaseEntity
	{
		public TripLog()
		{

		}

		public TripLog(Trip trip) : base()
		{
			TripId = trip.Id;
			TripRequestId = trip.TripRequestId;
			CustomerId = trip.CustomerId;
			DriverId = trip.DriverId;
			PaymentMethod = trip.PaymentMethod;
			TripStatus = trip.TripStatus;
			Source = trip.Source;
			Destination = trip.Destination;
			CabType = trip.CabType;
		}

		public Guid TripId { get; protected set; }
		public virtual Trip Trip { get; protected set; }
		public Guid TripRequestId { get; protected set; }
		public virtual TripRequest TripRequest { get; protected set; }
		public Guid CustomerId { get; protected set; }
		public virtual Customer Customer { get; protected set; }
		public Guid DriverId { get; protected set; }
		public virtual Driver Driver { get; protected set; }
		public PaymentMethod PaymentMethod { get; protected set; }
		public TripStatus TripStatus { get; protected set; }
		public Point Source { get; protected set; }
		public Point Destination { get; protected set; }
		public CabType CabType { get; protected set; }
	}
}
