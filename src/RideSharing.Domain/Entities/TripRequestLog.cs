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

		public Guid TripRequestId { get; protected set; }
		public Guid CustomerId { get; protected set; }
		public virtual Customer Customer { get; protected set; }
		public Point Source { get; protected set; }
		public Point Destination { get; protected set; }
		public CabType CabType { get; protected set; }
		public PaymentMethod PaymentMethod { get; protected set; }
		public TripRequestStatus Status { get; protected set; }
	}
}
