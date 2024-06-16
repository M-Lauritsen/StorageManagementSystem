using StorageManagement.API.SignalR;

namespace StorageManagement.Test.API;

public class PressenceTests
{
    private readonly PressenceTracker _pressenceTracker;
    public PressenceTests()
    {
        _pressenceTracker = new PressenceTracker();
        _pressenceTracker.Reset();
    }

    [Fact]
    public async Task UserConnected_ShouldAddNewUser()
    {
        // Arrange
        var username = "testuser";
        var connectionId = "connection1";

        // Act
        var isOnline = await _pressenceTracker.UserConnected(username, connectionId);

        // Assert
        Assert.True(isOnline);
        var onlineUsers = await _pressenceTracker.GetOnlineUsers();
        Assert.Contains(username, onlineUsers);
    }

    [Fact]
    public async Task UserConnected_ShouldAddConnectionToExistingUser()
    {
        // Arrange
        var username = "testuser";
        var connectionId1 = "connection1";
        var connectionId2 = "connection2";

        // Act
        await _pressenceTracker.UserConnected(username, connectionId1);
        var isOnline = await _pressenceTracker.UserConnected(username, connectionId2);

        // Assert
        Assert.False(isOnline);
        var connections = await _pressenceTracker.GetConnectionsForUser(username);
        Assert.Contains(connectionId1, connections);
        Assert.Contains(connectionId2, connections);
    }

    [Fact]
    public async Task UserDisconnected_ShouldRemoveConnection()
    {
        // Arrange
        var username = "testuser";
        var connectionId = "connection1";
        await _pressenceTracker.UserConnected(username, connectionId);

        // Act
        var isOffline = await _pressenceTracker.UserDisconnected(username, connectionId);

        // Assert
        Assert.True(isOffline);
        var onlineUsers = await _pressenceTracker.GetOnlineUsers();
        Assert.DoesNotContain(username, onlineUsers);
    }

    [Fact]
    public async Task GetOnlineUsers_ShouldReturnAllOnlineUsers()
    {
        // Arrange
        await _pressenceTracker.UserConnected("user1", "connection1");
        await _pressenceTracker.UserConnected("user2", "connection2");

        // Act
        var onlineUsers = await _pressenceTracker.GetOnlineUsers();

        // Assert
        Assert.Equal(2, onlineUsers.Length);
        Assert.Contains("user1", onlineUsers);
        Assert.Contains("user2", onlineUsers);
    }

    [Fact]
    public async Task UpdateUserRoute_ShouldUpdateRouteForUser()
    {
        // Arrange
        var username = "testuser";
        var connectionId = "connection1";
        var route = "/home";
        await _pressenceTracker.UserConnected(username, connectionId);

        // Act
        await _pressenceTracker.UpdateUserRoute(username, connectionId, route);

        // Assert
        var userRoute = await _pressenceTracker.GetUserRoute(connectionId);
        Assert.Equal(route, userRoute);
    }

    [Fact]
    public async Task GetUsersOnPage_ShouldReturnUsersOnSpecificPage()
    {
        // Arrange
        var route = "/home";
        await _pressenceTracker.UserConnected("user1", "connection1");
        await _pressenceTracker.UpdateUserRoute("user1", "connection1", route);

        await _pressenceTracker.UserConnected("user2", "connection2");
        await _pressenceTracker.UpdateUserRoute("user2", "connection2", route);

        // Act
        var usersOnPage = await _pressenceTracker.GetUsersOnPage(route);

        // Assert
        Assert.Contains("user1", usersOnPage);
        Assert.Contains("user2", usersOnPage);
    }

    [Fact]
    public async Task GetUserRoute_ShouldReturnRouteForConnectionId()
    {
        // Arrange
        var connectionId = "connection1";
        var route = "/home";
        await _pressenceTracker.UserConnected("user1", connectionId);
        await _pressenceTracker.UpdateUserRoute("user1", connectionId, route);

        // Act
        var userRoute = await _pressenceTracker.GetUserRoute(connectionId);

        // Assert
        Assert.Equal(route, userRoute);
    }
}
