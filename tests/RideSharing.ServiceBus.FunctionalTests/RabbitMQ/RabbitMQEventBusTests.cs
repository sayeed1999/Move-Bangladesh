using Microsoft.Extensions.Logging;
using Moq;
using RideSharing.ServiceBus.RabbitMQ;

namespace RideSharing.ServiceBus.FunctionalTests.RabbitMQ;

[Collection("Sequential")]
public class RabbitMQEventBusTests
{
	private readonly Mock<ILogger<RabbitMQEventBus>> _loggerMock = new Mock<ILogger<RabbitMQEventBus>>();
	private IList<MockEvent> receivedEvents = new List<MockEvent>();

	private readonly RabbitMQEventBus _eventBus;

	public RabbitMQEventBusTests()
	{
		_eventBus = new RabbitMQEventBus(_loggerMock.Object);
	}

	[Fact]
	public void SampleTest_One_ShouldEqual_One()
	{
		var x = 1;
		x.Should().Be(1);
	}

	[Fact]
	public async Task PublishMessageToQueue_TwoMessages_ConsumedSuccessfully()
	{
		var data = MockEvent1;
		var queue = "unit_test";

		var calledTimes = 0;

		var handler = async (MockEvent entity) =>
		{
			calledTimes++;
		};

		// Setup consumer
		await _eventBus.ConsumeAsync(handler, queue);

		// Publish two messages
		await _eventBus.PublishAsync(data, queue);
		await _eventBus.PublishAsync(data, queue);

		// keep the thread alive for 1s to finish processing all messages
		Thread.Sleep(1000);

		calledTimes.Should().Be(2);
	}

	[Fact]
	public async Task PublishMessageToQueue_ThreeMessages_ConsumedSuccessfully()
	{
		var data = MockEvent1;
		var queue = "unit_test";

		var calledTimes = 0;

		var handler = async (MockEvent entity) =>
		{
			calledTimes++;
		};

		// Setup consumer
		await _eventBus.ConsumeAsync(handler, queue);

		// Publish three messages
		await _eventBus.PublishAsync(data, queue);
		await _eventBus.PublishAsync(data, queue);
		await _eventBus.PublishAsync(data, queue);

		// keep the thread alive for 2s to finish processing all messages
		Thread.Sleep(2000);

		calledTimes.Should().Be(3);
	}
}
