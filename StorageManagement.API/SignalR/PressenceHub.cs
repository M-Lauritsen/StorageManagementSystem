using Microsoft.AspNetCore.SignalR;
using StorageManagement.API.Extensions;

namespace StorageManagement.API.SignalR;

public class PressenceHub(PressenceTracker _pressenceTracker) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var isOnline = await _pressenceTracker.UserConnected(Context.User?.GetUsername(), Context.ConnectionId);
        if (isOnline)
        {
            await Clients.Others.SendAsync("UserIsOnline", Context.User?.GetUsername());
        }

        var currentUsers = await _pressenceTracker.GetOnlineUsers();
        await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var isOffline = await _pressenceTracker.UserDisconnected(Context.User?.GetUsername(), Context.ConnectionId);

        if (isOffline)
        {
            await Clients.Others.SendAsync("UserIsOffline", Context.User?.GetUsername());
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task UpdateUserRoute(string route)
    {
        var connectionId = Context.ConnectionId;
        var username = Context.User?.GetUsername();

        var oldRoute = await _pressenceTracker.GetUserRoute(connectionId);
        if (!string.IsNullOrEmpty(oldRoute) && oldRoute != route)
        {
            await Groups.RemoveFromGroupAsync(connectionId, oldRoute);
        }

        await Groups.AddToGroupAsync(connectionId, route);
        await _pressenceTracker.UpdateUserRoute(username, connectionId, route);

        var usersOnPage = await _pressenceTracker.GetUsersOnPage(route);
        await Clients.Group(route).SendAsync("UsersOnPage", route, usersOnPage); // Include route

        // Update users on the old page
        if (!string.IsNullOrEmpty(oldRoute) && oldRoute != route)
        {
            var usersOnOldPage = await _pressenceTracker.GetUsersOnPage(oldRoute);
            await Clients.Group(oldRoute).SendAsync("UsersOnPage", oldRoute, usersOnOldPage); // Include old route
        }
    }
}
