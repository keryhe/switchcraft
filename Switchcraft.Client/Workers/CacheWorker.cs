using System.Net;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Switchcraft.Client.Clients;
using Switchcraft.Client.Dtos;
using Switchcraft.Client.Services;

namespace Switchcraft.Client.Workers;

internal record SwitchRecord(string Name, bool IsEnabled);
internal class CacheWorker: BackgroundService
{
    private readonly ISwitchClient _client;
    private readonly ILocalCache _cache;
    private readonly ICacheSignal _signal;
    private readonly ILogger<CacheWorker> _logger;
    
    private readonly HubConnection _connection;
    private readonly int? _environmentId;
    private readonly int? _applicationId;
    
    public CacheWorker(ISwitchClient client, ILocalCache cache, IConfiguration configuration, ICacheSignal signal, ILogger<CacheWorker> logger)
    {
        _client = client;
        _cache = cache;
        _signal = signal;
        _logger = logger;
        
        _environmentId = configuration.GetValue<int>("Switchcraft:EnvironmentId");
        _applicationId = configuration.GetValue<int>("Switchcraft:ApplicationId");
        
        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7280/hubs/notification")
            .WithAutomaticReconnect()
            .Build();
        _connection.On<SwitchRecord>("EnabledChanged", (message) =>
        {
            _cache.AddOrUpdate(message.Name, message.IsEnabled);
            _logger.LogDebug(message.Name + ": IsEnabled = " + message.IsEnabled);
        });
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if(_environmentId == null || _applicationId == null)
        {
            throw new NullReferenceException("Environment and/or Application is null");
        }
        
        await InitializeCache();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _connection.StartAsync(stoppingToken);
                _logger.LogInformation("SignalR connection established.");

                await _connection.InvokeAsync("JoinGroup", _environmentId.Value, _applicationId.Value, stoppingToken);
                _logger.LogInformation("Joined SignalR group: " + _environmentId.Value + "_" + _applicationId.Value);

                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug("Service cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SignalR connection error. Retrying...");
                await Task.Delay(5000, stoppingToken);
            }
        }
        
        if (_connection.State == HubConnectionState.Connected)
        {
            await _connection.StopAsync(stoppingToken);
        }
        _logger.LogInformation("SignalR client worker stopped.");
    }

    private async Task InitializeCache()
    {
        _logger.LogInformation("Initializing cache.");
        
        _signal.Wait();
        
        var switches = await _client.GetSwitchesAsync(_environmentId!.Value, _applicationId!.Value);
        try
        {
            foreach (SwitchDto dto in switches)
            {
                _cache.AddOrUpdate(dto.Name, dto.IsEnabled);
            }
        }
        finally
        {
            _signal.Release();
        }
    }
}