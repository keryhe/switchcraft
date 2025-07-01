using System;

namespace Switchcraft.Server.Controllers.Dtos;

public class EnvironmentResponse
{
    public EnvironmentResponse()
    {        
    }

    public EnvironmentResponse(Data.Models.Environment env)
    {        
        Id = env.Id;
        Name = env.Name;
        CreatedAt = env.CreatedAt;
        UpdatedAt = env.UpdatedAt;
    }

    public int Id { get; set; }
    
    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}
