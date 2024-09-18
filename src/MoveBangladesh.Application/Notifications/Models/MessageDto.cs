namespace MoveBangladesh.Application.Notifications.Models;

public class MessageDto
{
	public required string From { get; set; }
	public required string To { get; set; }
	public string? Subject { get; set; }
	public string? Body { get; set; }
}
