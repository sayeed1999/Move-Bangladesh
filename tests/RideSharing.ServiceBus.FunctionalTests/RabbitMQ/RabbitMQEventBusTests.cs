namespace RideSharing.ServiceBus.FunctionalTests.RabbitMQ;

public class RabbitMQEventBusTests
{
	[Fact]
	public void SampleTest_One_ShouldEqual_One()
	{
		var x = 1;
		x.Should().Be(1);
	}


}
