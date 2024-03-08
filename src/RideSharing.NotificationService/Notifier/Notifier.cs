namespace RideSharing.NotificationService.Notifier
{
	internal class Notifier : INotifier
	{
		public Task Notify(string message)
		{
			Console.WriteLine("[x] - Person notified !");
			return Task.CompletedTask;
		}
	}
}
