using System;
using Switchcraft.Data.Models;

namespace Switchcraft.Server.Controllers.Dtos;

public class SwitchInstanceResponse
{
    public SwitchInstanceResponse()
    {
        Name = string.Empty;
    }

    public SwitchInstanceResponse(SwitchInstance switchInstance)
    {
        Id = switchInstance.Id;
        Name = switchInstance.Switch?.Name ?? string.Empty;
        IsEnabled = switchInstance.IsEnabled;
        Environment = (switchInstance.Environment != null) ? new EnvironmentResponse(switchInstance.Environment) : null;
        CreatedAt = switchInstance.CreatedAt;
        UpdatedAt = switchInstance.UpdatedAt;
    }

    public int Id { get; set; }
    
    public string Name { get; set; }

    public bool IsEnabled { get; set; }

    public EnvironmentResponse? Environment { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}
