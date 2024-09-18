namespace MoveBangladesh.NotificationService.Notifier
{
	public interface INotifier
	{
		Task Notify(string message);
	}
}
