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
	public async Task PublishMessageToQueue_TwoMessages_ConsumedSuccessfully()
	{
		var data = MockEvent1;
		var queue = "unit_test_1";

		var calledTimes = 0;

		var handler = async (MockEvent entity) =>
		{
			calledTimes++;
		};

		// Publish two messages
		await _eventBus.PublishAsync(data, queue);
		await _eventBus.PublishAsync(data, queue);

		// Setup consumer
		await _eventBus.ConsumeAsync(handler, queue);

		// keep the thread alive for 0.5s to finish processing all messages
		Thread.Sleep(500);

		calledTimes.Should().Be(2);
	}

	[Fact]
	public async Task PublishMessageToQueue_ThreeMessages_ConsumedSuccessfully()
	{
		var data = MockEvent1;
		var queue = "unit_test_2";

		var calledTimes = 0;

		var handler = async (MockEvent entity) =>
		{
			calledTimes++;
		};

		// Publish three messages
		await _eventBus.PublishAsync(data, queue);
		await _eventBus.PublishAsync(data, queue);
		await _eventBus.PublishAsync(data, queue);

		// Setup consumer
		await _eventBus.ConsumeAsync(handler, queue);

		// keep the thread alive for 0.5s to finish processing all messages
		Thread.Sleep(500);

		calledTimes.Should().Be(3);
	}
}
