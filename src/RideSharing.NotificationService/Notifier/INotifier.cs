namespace RideSharing.NotificationService.Notifier
{
	internal interface INotifier
	{
		Task Notify(string message);
	}
}
