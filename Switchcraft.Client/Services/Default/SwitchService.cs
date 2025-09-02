using System.Net;
using Microsoft.Extensions.Logging;
using Switchcraft.Client.Clients;

namespace Switchcraft.Client.Services.Default;

internal class SwitchService : ISwitchService
{
    private readonly ILocalCache _cache; 
    private readonly ICacheSignal _signal;
    private readonly ILogger<SwitchService> _logger;

    public SwitchService(ILocalCache cache, ICacheSignal signal, ILogger<SwitchService> logger)
    {
        _cache = cache;
        _signal = signal;
        _logger = logger;
    }
    
    public bool GetSwitch(string name, bool defaultValue = false)
    {
        _signal.Wait();
        try
        {
            return _cache.GetValue(name);
        }
        catch
        {
            _logger.LogWarning("Switch '{name}' not found. Using default value '{defaultValue}'", name, defaultValue);
        }
        finally
        {
            _signal.Release();
        }
        return defaultValue;
    }
}