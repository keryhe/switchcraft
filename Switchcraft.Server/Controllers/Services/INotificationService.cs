using Switchcraft.Data.Models;

namespace Switchcraft.Server.Controllers.Services;

public interface INotificationService
{
    Task NotifyIsEnabledChanged(SwitchInstance switchInstance);
    Task NotifyIsEnabledChanged(IEnumerable<SwitchInstance> switchInstances);
}