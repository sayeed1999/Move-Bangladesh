using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Processor.UnitTests.TransitionChecker
{
	public class TripStatusTransitionCheckerTests
	{
		private TripStatusTransitionChecker _checker;

		public TripStatusTransitionCheckerTests()
		{
			_checker = new TripStatusTransitionChecker();
		}

		[Theory]
		[InlineData(TripStatus.DriverAccepted, TripStatus.CustomerCanceled)]
		[InlineData(TripStatus.DriverAccepted, TripStatus.DriverCanceled)]
		[InlineData(TripStatus.DriverAccepted, TripStatus.TripStarted)]
		[InlineData(TripStatus.TripStarted, TripStatus.TripCompleted)]
		public void ValidPath_ReturnsTrue(TripStatus fromStatus, TripStatus toStatus)
		{
			bool result = _checker.IsTransitionValid(fromStatus, toStatus);

			Assert.True(result);
		}

		[Theory]
		[InlineData(TripStatus.DriverAccepted, TripStatus.TripCompleted)]
		[InlineData(TripStatus.CustomerCanceled, TripStatus.DriverAccepted)]
		[InlineData(TripStatus.DriverCanceled, TripStatus.DriverAccepted)]
		[InlineData(TripStatus.TripStarted, TripStatus.DriverAccepted)]
		[InlineData(TripStatus.TripCompleted, TripStatus.DriverAccepted)]
		public void InvalidPath_ReturnsFalse(TripStatus fromStatus, TripStatus toStatus)
		{
			bool result = _checker.IsTransitionValid(fromStatus, toStatus);

			Assert.False(result);
		}
	}
}
