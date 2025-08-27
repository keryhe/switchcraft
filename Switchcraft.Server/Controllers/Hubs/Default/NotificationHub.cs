using Microsoft.AspNetCore.SignalR;

namespace Switchcraft.Server.Controllers.Hubs.Default;

public class NotificationHub: Hub<INotificationHub>
{
    public async Task JoinGroup(int environmentId, int applicationId)
    {
        var groupName = environmentId + "_" + applicationId;
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
}