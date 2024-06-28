namespace RideSharing.NotificationService.Notifier
{
	public interface INotifier
	{
		Task Notify(string message);
	}
}
