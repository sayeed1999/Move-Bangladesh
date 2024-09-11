using RideSharing.Application.Abstractions;
using RideSharing.Application.Notifications.Models;

namespace RideSharing.Infrastructure;

public class NotificationService : INotificationService
{
	public Task SendAsync(MessageDto messageDto)
	{
		// TODO: - do something, atleast use logger to log that notification sent!
		return Task.CompletedTask;
	}
}
