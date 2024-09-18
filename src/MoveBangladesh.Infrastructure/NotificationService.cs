using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Application.Notifications.Models;

namespace MoveBangladesh.Infrastructure;

public class NotificationService : INotificationService
{
	public Task SendAsync(MessageDto messageDto)
	{
		// TODO: - do something, atleast use logger to log that notification sent!
		return Task.CompletedTask;
	}
}
