using System;
using Switchcraft.Data.Models;

namespace Switchcraft.Server.Controllers.Dtos;

public class InstanceSwitchResponse
{
    public InstanceSwitchResponse()
    {
    }

    public InstanceSwitchResponse(SwitchInstance switchInstance)
    {
        Id = switchInstance.Id;
        IsEnabled = switchInstance.IsEnabled;
        Name = switchInstance.Switch?.Name;
        CreatedAt = switchInstance.CreatedAt;
        UpdatedAt = switchInstance.UpdatedAt;
    }

    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

}
