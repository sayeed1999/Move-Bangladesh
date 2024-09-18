namespace MoveBangladesh.PushService.Abstraction;

public interface IPushService
{
    Task AddUserToGroup(string group);
    Task RemoveUserFromAllGroups();
    Task RemoveUserFromGroup(string group);
    Task SendMessageToCurrentUser(string message);
    Task SendMessageToGroup(string group, string message);
    Task BroadcastMessageToAll(string message);
}
