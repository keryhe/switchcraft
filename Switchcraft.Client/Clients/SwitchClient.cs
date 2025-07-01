using System;
using System.Net.Http.Json;
using Switchcraft.Client.Dtos;
using Microsoft.Extensions.Logging;

namespace Switchcraft.Client.Clients;

internal interface ISwitchClient
{
    Task<IEnumerable<SwitchDto>> GetSwitchesAsync(int environmentId, int applicationId);
}

internal class SwitchClient : ISwitchClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SwitchClient> _logger;

    public SwitchClient(HttpClient httpClient, ILogger<SwitchClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<SwitchDto>> GetSwitchesAsync(int environmentId, int applicationId)
    {
        List<SwitchDto> result = new List<SwitchDto>();
        try
        {
            string requestUri = $"api/SwitchInstances?environmentId=" + environmentId + "&applicationId=" + applicationId ;
            _logger.LogDebug("Calling API: {BaseAddress}{RequestUri}", _httpClient.BaseAddress, requestUri);
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var switches = await response.Content.ReadFromJsonAsync<IEnumerable<SwitchDto>>();
                if (switches != null)
                {
                    result.AddRange(switches);
                }
            }
            else
            {
                _logger.LogError("GetSwitches failed with status code: {StatusCode}", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSwitches failed");
        }
        return result;
    }
}
