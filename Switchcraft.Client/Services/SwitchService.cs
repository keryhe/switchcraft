using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Switchcraft.Client.Dtos;

namespace Switchcraft.Client.Services;

public interface ISwitchService
{
    IAsyncEnumerable<SwitchDto> GetSwitches(Func<SwitchDto, bool>? filter = null);
}

internal class SwitchService : ISwitchService
{
    private readonly IMemoryCache _cache; 
    private readonly CacheSignal<SwitchDto> _signal;
    private readonly ILogger<SwitchService> _logger;

    public SwitchService(IMemoryCache cache, CacheSignal<SwitchDto> signal, ILogger<SwitchService> logger)
    {
        _cache = cache;
        _signal = signal;
        _logger = logger;
    }

    public async IAsyncEnumerable<SwitchDto> GetSwitches(Func<SwitchDto, bool>? filter = null)
    {
        try
        {
            await _signal.WaitAsync();
            SwitchDto[] switches = (
                await _cache.GetOrCreateAsync("Switchcraft", _ =>
                {
                    return Task.FromResult(Array.Empty<SwitchDto>());
                })
            )!;

            // If no filter is provided, use a pass-thru.
            filter ??= _ => true;

            foreach (SwitchDto s in switches)
            {
                if (filter(s))
                {
                    yield return s;
                }
            }
        }
        finally
        {
            _signal.Release();
        }
    }
}
