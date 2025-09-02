using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Switchcraft.Client.Dtos;
using Switchcraft.Client.Services;

namespace Switchcraft.Client;

public interface ISwitch
{
    bool On(string name, bool defaultValue = false);
    bool Off(string name, bool defaultValue = false);
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
        var result = IsOn(name, defaultValue);
        return result;
    }

    public bool Off(string name, bool defaultValue = false)
    {
        var result = IsOn(name, defaultValue);
        return !result;
    }

    private bool IsOn(string name, bool defaultValue = false)
    {
        var result = _service.GetSwitch(name, defaultValue);
        return result;
    }
}
