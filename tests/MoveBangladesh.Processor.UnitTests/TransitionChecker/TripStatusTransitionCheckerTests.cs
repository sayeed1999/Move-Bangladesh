using MoveBangladesh.Domain.Entities;
using MoveBangladesh.Processor.TransitionChecker;

namespace MoveBangladesh.Processor.UnitTests.TransitionChecker
{
	public class TripStatusTransitionCheckerTests
	{
		private TripStatusTransitionChecker _checker;

		public TripStatusTransitionCheckerTests()
		{
			_checker = new TripStatusTransitionChecker();
		}

		[Theory]
		[InlineData(TripStatus.ONGOING, TripStatus.WAITING_FOR_PAYMENT)]
		[InlineData(TripStatus.WAITING_FOR_PAYMENT, TripStatus.PAYMENT_COMPLETED)]
		public void ValidPath_ReturnsTrue(TripStatus fromStatus, TripStatus toStatus)
		{
			bool result = _checker.IsTransitionValid(fromStatus, toStatus);

			Assert.True(result);
		}

		[Theory]
		[InlineData(TripStatus.ONGOING, TripStatus.PAYMENT_COMPLETED)]
		[InlineData(TripStatus.WAITING_FOR_PAYMENT, TripStatus.ONGOING)]
		[InlineData(TripStatus.PAYMENT_COMPLETED, TripStatus.WAITING_FOR_PAYMENT)]
		public void InvalidPath_ReturnsFalse(TripStatus fromStatus, TripStatus toStatus)
		{
			bool result = _checker.IsTransitionValid(fromStatus, toStatus);

			Assert.False(result);
		}
	}
}
