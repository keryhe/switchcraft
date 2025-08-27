using Microsoft.AspNetCore.SignalR;
using Switchcraft.Data.Models;
using Switchcraft.Server.Controllers.Hubs;
using Switchcraft.Server.Controllers.Hubs.Default;

namespace Switchcraft.Server.Controllers.Services.Default;

public class NotificationService: INotificationService
{
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub, INotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task NotifyIsEnabledChanged(SwitchInstance switchInstance)
    {
        var groupName = switchInstance.EnvironmentId + "_" + switchInstance.Switch!.ApplicationId;
        var record = new SwitchRecord(switchInstance.Switch!.Name, switchInstance.IsEnabled);
        
        await _hubContext.Clients.Group(groupName).EnabledChanged(record);
    }

    public async Task NotifyIsEnabledChanged(IEnumerable<SwitchInstance> switchInstances)
    {
        
        foreach (var switchInstance in switchInstances)
        {
            var groupName = switchInstance.EnvironmentId + "_" + switchInstance.Switch!.ApplicationId;
            var record = new SwitchRecord(switchInstance.Switch!.Name, switchInstance.IsEnabled);
            await _hubContext.Clients.Group(groupName).EnabledChanged(record);
        }
    }
}