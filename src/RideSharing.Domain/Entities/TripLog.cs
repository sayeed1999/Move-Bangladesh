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

		public Guid TripId { get; set; }
		public virtual Trip Trip { get; set; }
		public Guid TripRequestId { get; set; }
		public virtual TripRequest TripRequest { get; set; }
		public Guid CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
		public Guid DriverId { get; set; }
		public virtual Driver Driver { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public TripStatus TripStatus { get; set; }
		public Point Source { get; set; }
		public Point Destination { get; set; }
		public CabType CabType { get; set; }
	}
}
