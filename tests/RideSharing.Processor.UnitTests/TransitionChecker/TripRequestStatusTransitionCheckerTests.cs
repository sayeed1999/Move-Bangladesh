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
		[InlineData(TripRequestStatus.NoDriverAccepted, TripRequestStatus.CustomerCanceledBeforeDriverFound)]
		[InlineData(TripRequestStatus.NoDriverAccepted, TripRequestStatus.DriverAccepted)]
		[InlineData(TripRequestStatus.DriverAccepted, TripRequestStatus.CustomerCanceledAfterDriverFound)]
		[InlineData(TripRequestStatus.DriverAccepted, TripRequestStatus.DriverCanceled)]
		[InlineData(TripRequestStatus.DriverCanceled, TripRequestStatus.NoDriverAccepted)]
		public void ValidPath_ReturnsTrue(TripRequestStatus fromStatus, TripRequestStatus toStatus)
		{
			bool result = _checker.IsTransitionValid(fromStatus, toStatus);

			Assert.True(result);
		}

		[Theory]
		[InlineData(TripRequestStatus.NoDriverAccepted, TripRequestStatus.CustomerCanceledAfterDriverFound)]
		[InlineData(TripRequestStatus.NoDriverAccepted, TripRequestStatus.DriverCanceled)]
		[InlineData(TripRequestStatus.CustomerCanceledBeforeDriverFound, TripRequestStatus.DriverAccepted)]
		[InlineData(TripRequestStatus.CustomerCanceledBeforeDriverFound, TripRequestStatus.CustomerCanceledAfterDriverFound)]
		[InlineData(TripRequestStatus.CustomerCanceledBeforeDriverFound, TripRequestStatus.DriverCanceled)]
		[InlineData(TripRequestStatus.CustomerCanceledAfterDriverFound, TripRequestStatus.NoDriverAccepted)]
		[InlineData(TripRequestStatus.CustomerCanceledAfterDriverFound, TripRequestStatus.DriverAccepted)]
		[InlineData(TripRequestStatus.CustomerCanceledAfterDriverFound, TripRequestStatus.CustomerCanceledBeforeDriverFound)]
		[InlineData(TripRequestStatus.CustomerCanceledAfterDriverFound, TripRequestStatus.DriverCanceled)]
		[InlineData(TripRequestStatus.DriverAccepted, TripRequestStatus.NoDriverAccepted)]
		[InlineData(TripRequestStatus.DriverAccepted, TripRequestStatus.CustomerCanceledBeforeDriverFound)]
		[InlineData(TripRequestStatus.DriverCanceled, TripRequestStatus.CustomerCanceledBeforeDriverFound)]
		[InlineData(TripRequestStatus.DriverCanceled, TripRequestStatus.CustomerCanceledAfterDriverFound)]
		[InlineData(TripRequestStatus.DriverCanceled, TripRequestStatus.DriverAccepted)]
		public void InvalidPath_ReturnsFalse(TripRequestStatus fromStatus, TripRequestStatus toStatus)
		{
			bool result = _checker.IsTransitionValid(fromStatus, toStatus);

			Assert.False(result);
		}
	}
}
