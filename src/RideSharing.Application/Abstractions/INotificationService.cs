using RideSharing.Application.Notifications.Models;

namespace RideSharing.Application.Abstractions;

public interface INotificationService
{
	Task SendAsync(MessageDto messageDto);
}
