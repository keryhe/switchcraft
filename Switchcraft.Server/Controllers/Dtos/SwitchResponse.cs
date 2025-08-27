using System;
using Switchcraft.Data.Models;

namespace Switchcraft.Server.Controllers.Dtos;

public class SwitchResponse
{
    public SwitchResponse()
    {
        Name = string.Empty;
        Instances = new List<SwitchInstanceResponse>();
    }

    public SwitchResponse(Switch s)
    {
        Id = s.Id;
        Name = s.Name;
        Application = (s.Application != null) ? new ApplicationResponse(s.Application) : null;
        CreatedAt = s.CreatedAt;
        UpdatedAt = s.UpdatedAt;
        Instances = new List<SwitchInstanceResponse>();

        foreach(SwitchInstance switchInstance in s.SwitchInstances)
        {
            Instances.Add(new SwitchInstanceResponse(switchInstance));
        }
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public ApplicationResponse? Application { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; } 

    public List<SwitchInstanceResponse> Instances { get; set; }
}
