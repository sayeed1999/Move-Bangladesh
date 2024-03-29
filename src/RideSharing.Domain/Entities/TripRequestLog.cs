using NetTopologySuite.Geometries;
using RideSharing.Domain.Enums;

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

		public Guid TripRequestId { get; set; }
		public Guid CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
		public Point Source { get; set; }
		public Point Destination { get; set; }
		public CabType CabType { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public TripRequestStatus Status { get; set; }
	}
}
