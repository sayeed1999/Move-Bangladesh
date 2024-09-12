using NetTopologySuite.Geometries;

namespace RideSharing.Domain.Entities
{
	public class TripRequestLog : BaseEntity
	{
		public TripRequestLog()
		{

		}
		public TripRequestLog(TripRequest tripRequest)
		{
			TripRequestId = tripRequest.Id;
			CustomerId = tripRequest.CustomerId;
			Source = tripRequest.Source;
			Destination = tripRequest.Destination;
			CabType = tripRequest.CabType;
			PaymentMethod = tripRequest.PaymentMethod;
			Status = tripRequest.Status;
		}

		public required string TripRequestId { get; set; }
		public virtual TripRequest TripRequest { get; set; }
		public required string CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
		public string? DriverId { get; set; }
		public virtual Driver? Driver { get; set; }
		public Point Source { get; set; }
		public Point Destination { get; set; }
		public CabType CabType { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public TripRequestStatus Status { get; set; }
	}
}
