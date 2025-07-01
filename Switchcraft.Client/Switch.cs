using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Switchcraft.Client.Dtos;
using Switchcraft.Client.Services;

namespace Switchcraft.Client;

public interface ISwitch
{
    bool On(string name, bool defaultValue = false);
    Task<bool> OnAsync(string name, bool defaultValue = false);

    bool Off(string name, bool defaultValue = false);
    Task<bool> OffAsync(string name, bool defaultValue = false);
}

public class Switch : ISwitch
{
    private readonly ISwitchService _service;
    private readonly ILogger<Switch> _logger;

    public Switch(ISwitchService service, ILogger<Switch> logger)
    {
        _service = service;
        _logger = logger;
    }

    public bool On(string name, bool defaultValue = false)
    {
        return IsOn(name, defaultValue);
    }

    public async Task<bool> OnAsync(string name, bool defaultValue = false)
    {
        return await IsOnAsync(name, defaultValue);
    }

    public bool Off(string name, bool defaultValue = false)
    {
        var result = IsOn(name, defaultValue);
        return !result;
    }

    public async Task<bool> OffAsync(string name, bool defaultValue = false)
    {
        var result = await IsOnAsync(name, defaultValue);
        return !result;
    }

    private bool IsOn(string name, bool defaultValue = false)
    {
        bool result = defaultValue;
        
        var switchesAsync = _service.GetSwitches(f => f.Name == name);
        var switches = switchesAsync.ToBlockingEnumerable();
        foreach(SwitchDto s in switches)
        {
            result = s.IsEnabled;
        }

        return result;
    }

    private async Task<bool> IsOnAsync(string name, bool defaultValue = false)
    {
        bool result = defaultValue;
        
        var switches = _service.GetSwitches(f => f.Name == name);
        await foreach(SwitchDto s in switches)
        {
            result = s.IsEnabled;
        }

        return result;
    }
}
