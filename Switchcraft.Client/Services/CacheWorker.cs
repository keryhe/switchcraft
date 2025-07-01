using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Switchcraft.Client.Clients;
using Switchcraft.Client.Dtos;

namespace Switchcraft.Client.Services;

internal class CacheWorker : BackgroundService
{
    private readonly ISwitchClient _client;
    private readonly CacheSignal<SwitchDto> _signal;
    private readonly IMemoryCache _cache;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CacheWorker> _logger;

    private readonly int? _environmentId;
    private readonly int? _applicationId;
    private readonly TimeSpan _updateInterval;

    private bool _isCacheInitialized = false;

    public CacheWorker(ISwitchClient client, CacheSignal<SwitchDto> signal, IMemoryCache cache, IConfiguration configuration, ILogger<CacheWorker> logger)
    {
        _client = client;
        _signal = signal;
        _cache = cache;
        _configuration = configuration;
        _logger = logger;

        _environmentId = _configuration.GetValue<int>("Switchcraft:EnvironmentId");
        _applicationId = _configuration.GetValue<int>("Switchcraft:ApplicationId");
        var period = _configuration.GetValue<int>("Switchcraft:Period");
        _updateInterval = TimeSpan.FromMinutes(period);

    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await _signal.WaitAsync();
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Updating cache.");

            try
            {
                if(_environmentId == null || _applicationId == null)
                {
                    throw new NullReferenceException("Environment and/or Application is null");
                }
                
                SwitchDto[] switches = (await _client.GetSwitchesAsync(_environmentId.Value, _applicationId.Value)).ToArray();

                if(switches is { Length: > 0 })
                {
                    _cache.Set("Switchcraft", switches);
                    _logger.LogDebug("Cache updated with {Count:#,#} flags.", switches.Length);
                }
                else
                {
                    _logger.LogWarning("Unable to fetch flags to update cache");
                }
            }
            finally
            {
                if(!_isCacheInitialized)
                {
                    _signal.Release();
                    _isCacheInitialized = true;
                }  
            }

            _logger.LogInformation("Will attempt to update the cache in {Minutes} minutes.", _updateInterval.Minutes);
            await Task.Delay(_updateInterval, stoppingToken);
        }
    }
}
