using RideSharing.ServiceBus.Abstractions;

namespace RideSharing.ServiceBus.FunctionalTests;

public static class ConstantData
{
	public static MockEvent MockEvent1 = new MockEvent(1, "ABC");
	public static MockEvent MockEvent2 = new MockEvent(2, "DEF");
}

public class MockEvent : IEvent
{
	public MockEvent(int id, string name)
	{
		Id = id;
		Name = name;
	}

	public int Id { get; set; }
	public string Name { get; set; }
}