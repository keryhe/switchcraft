using System;
using System.Text.Json.Serialization;

namespace Switchcraft.Client.Dtos;

public class SwitchDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("is_enabled")]
    public bool IsEnabled { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }
    
    [JsonPropertyName("update_at")]
    public DateTime? UpdatedAt { get; set; }
}
