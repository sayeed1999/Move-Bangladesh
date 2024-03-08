namespace RideSharing.ServiceBus.Abstractions
{
	public interface IIntegrationEvent
	{
		Guid Id { get; init; }
	}

	/// <summary>
	/// The abstract IntegrationEvent serves as a base class for concrete implementations.
	/// </summary>
	/// <param name="Id"></param>
	public abstract record IntegrationEvent(Guid Id) : IIntegrationEvent;
}
