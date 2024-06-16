using Microsoft.AspNetCore.SignalR;

namespace StorageManagement.API.SignalR;

public class PressenceHub(PressenceTracker _pressenceTracker) : Hub
{
}
