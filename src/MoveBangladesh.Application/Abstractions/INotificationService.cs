using MoveBangladesh.Application.Notifications.Models;

namespace MoveBangladesh.Application.Abstractions;

public interface INotificationService
{
	Task SendAsync(MessageDto messageDto);
}
