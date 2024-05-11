using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Processor.UnitTests.TransitionChecker
{
	public class TripRequestStatusTransitionCheckerTests
	{
		private TripRequestStatusTransitionChecker _checker;

		public TripRequestStatusTransitionCheckerTests()
		{
			_checker = new TripRequestStatusTransitionChecker();
		}

		[Theory]
		[InlineData(TripRequestStatus.NO_DRIVER_FOUND, TripRequestStatus.CUSTOMER_CANCELED)]
		[InlineData(TripRequestStatus.NO_DRIVER_FOUND, TripRequestStatus.DRIVER_ACCEPTED)]
		[InlineData(TripRequestStatus.DRIVER_ACCEPTED, TripRequestStatus.CUSTOMER_REJECTED_DRIVER)]
		[InlineData(TripRequestStatus.DRIVER_ACCEPTED, TripRequestStatus.DRIVER_REJECTED_CUSTOMER)]
		[InlineData(TripRequestStatus.DRIVER_REJECTED_CUSTOMER, TripRequestStatus.NO_DRIVER_FOUND)]
		public void ValidPath_ReturnsTrue(TripRequestStatus fromStatus, TripRequestStatus toStatus)
		{
			bool result = _checker.IsTransitionValid(fromStatus, toStatus);

			Assert.True(result);
		}

		[Theory]
		[InlineData(TripRequestStatus.NO_DRIVER_FOUND, TripRequestStatus.CUSTOMER_REJECTED_DRIVER)]
		[InlineData(TripRequestStatus.NO_DRIVER_FOUND, TripRequestStatus.DRIVER_REJECTED_CUSTOMER)]
		[InlineData(TripRequestStatus.CUSTOMER_CANCELED, TripRequestStatus.DRIVER_ACCEPTED)]
		[InlineData(TripRequestStatus.CUSTOMER_CANCELED, TripRequestStatus.CUSTOMER_REJECTED_DRIVER)]
		[InlineData(TripRequestStatus.CUSTOMER_CANCELED, TripRequestStatus.DRIVER_REJECTED_CUSTOMER)]
		[InlineData(TripRequestStatus.CUSTOMER_REJECTED_DRIVER, TripRequestStatus.NO_DRIVER_FOUND)]
		[InlineData(TripRequestStatus.CUSTOMER_REJECTED_DRIVER, TripRequestStatus.DRIVER_ACCEPTED)]
		[InlineData(TripRequestStatus.CUSTOMER_REJECTED_DRIVER, TripRequestStatus.CUSTOMER_CANCELED)]
		[InlineData(TripRequestStatus.CUSTOMER_REJECTED_DRIVER, TripRequestStatus.DRIVER_REJECTED_CUSTOMER)]
		[InlineData(TripRequestStatus.DRIVER_ACCEPTED, TripRequestStatus.NO_DRIVER_FOUND)]
		[InlineData(TripRequestStatus.DRIVER_ACCEPTED, TripRequestStatus.CUSTOMER_CANCELED)]
		[InlineData(TripRequestStatus.DRIVER_REJECTED_CUSTOMER, TripRequestStatus.CUSTOMER_CANCELED)]
		[InlineData(TripRequestStatus.DRIVER_REJECTED_CUSTOMER, TripRequestStatus.CUSTOMER_REJECTED_DRIVER)]
		[InlineData(TripRequestStatus.DRIVER_REJECTED_CUSTOMER, TripRequestStatus.DRIVER_ACCEPTED)]
		public void InvalidPath_ReturnsFalse(TripRequestStatus fromStatus, TripRequestStatus toStatus)
		{
			bool result = _checker.IsTransitionValid(fromStatus, toStatus);

			Assert.False(result);
		}
	}
}
