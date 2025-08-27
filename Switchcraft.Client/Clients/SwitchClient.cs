using System;
using System.Net.Http.Json;
using Switchcraft.Client.Dtos;
using Microsoft.Extensions.Logging;

namespace Switchcraft.Client.Clients;

internal interface ISwitchClient
{
    Task<IEnumerable<SwitchDto>> GetSwitchesAsync(int environmentId, int applicationId);
    Task<SwitchDto?> GetSwitchAsync(string name);
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

    public async Task<SwitchDto?> GetSwitchAsync(string name)
    {
        try
        {
            string requestUri = $"api/SwitchInstances/{name}";
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<SwitchDto>();
                return content;
            }
            else
            {
                _logger.LogError("GetSwitch failed with status code: {StatusCode}", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSwitch failed");
        }
        
        return null;
    }
}
