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
        bool result = this.IsOnAsync(name, defaultValue).Result;
        
        return result;
    }

    private async Task<bool> IsOnAsync(string name, bool defaultValue = false)
    {
        bool result;
        try
        {
            result = await _service.GetSwitchAsync(name);
        }
        catch (KeyNotFoundException)
        {
            result = defaultValue;
            _logger.LogError("Switch {name} was not found", name);
        }

        return result;
    }
}
