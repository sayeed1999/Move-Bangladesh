using Microsoft.AspNetCore.SignalR;
using RideSharing.PushService.Abstraction;

namespace RideSharing.PushService.SignalR;

public abstract class HubBase : Hub, IPushService
{
    private readonly Dictionary<string, HashSet<string>> userToGroupsMap;
    
    public HubBase()
    {
        userToGroupsMap = new Dictionary<string, HashSet<string>>();
    }

	public override async Task OnConnectedAsync()
	{
        string group = GetGroupName();

        await AddUserToGroup(group);

		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception exception)
	{
        await RemoveUserFromAllGroups();

		await base.OnDisconnectedAsync(exception);
	}

	public async Task AddUserToGroup(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, group);

        lock (userToGroupsMap)
        {
            if (!userToGroupsMap.ContainsKey(Context.ConnectionId))
            {
                userToGroupsMap[Context.ConnectionId] = new HashSet<string>();
            }
            userToGroupsMap[Context.ConnectionId].Add(group);
        }
    }

    public async Task RemoveUserFromAllGroups()
    {
        if (userToGroupsMap.ContainsKey(Context.ConnectionId))
        {
            foreach (var group in userToGroupsMap[Context.ConnectionId])
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            }
        }

        lock (userToGroupsMap)
        {
            userToGroupsMap.Remove(Context.ConnectionId);
        }
    }

    public async Task RemoveUserFromGroup(string group)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);

        lock (userToGroupsMap)
        {
            var groupsForUser = userToGroupsMap.GetValueOrDefault(Context.ConnectionId);
            groupsForUser?.Remove(group);
        }
    }

    public async Task SendMessageToCurrentUser(string message)
    {
        await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", message);
    }

    public async Task SendMessageToGroup(string group, string message)
    {
        await Clients.Group(group).SendAsync("ReceiveMessage", message);
    }

    public async Task BroadcastMessageToAll(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    private string GetGroupName()
    {
        return Context.GetHttpContext().Request.Query["group"];
    }
}
