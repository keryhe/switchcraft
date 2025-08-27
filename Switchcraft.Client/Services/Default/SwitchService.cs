using Microsoft.Extensions.Logging;
using Switchcraft.Client.Clients;

namespace Switchcraft.Client.Services.Default;

internal class SwitchService : ISwitchService
{
    private readonly ILocalCache _cache; 
    private readonly ISwitchClient _client;
    private readonly ILogger<SwitchService> _logger;

    public SwitchService(ILocalCache cache, ISwitchClient client, ILogger<SwitchService> logger)
    {
        _cache = cache;
        _client = client;
        _logger = logger;
    }
    
    public async Task<bool> GetSwitchAsync(string name)
    {
        var exists = _cache.TryGetValue(name, out bool result);
        if (!exists)
        {
            _logger.LogWarning("Switch {name} not found. Calling API...", name);
            var dto = await _client.GetSwitchAsync(name);

            if (dto == null)
            {
                throw new KeyNotFoundException();
            }
            _cache.AddOrUpdate(name, dto.IsEnabled);
            return dto.IsEnabled;
        }
        
        return result;
    }
}