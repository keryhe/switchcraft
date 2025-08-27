namespace Switchcraft.Server.Controllers.Hubs;

public interface INotificationHub
{
    Task EnabledChanged(SwitchRecord record);
}

public record SwitchRecord(string Name, bool IsEnabled);