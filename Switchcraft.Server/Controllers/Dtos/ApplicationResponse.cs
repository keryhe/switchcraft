using System;
using Switchcraft.Data.Models;

namespace Switchcraft.Server.Controllers.Dtos;

public class ApplicationResponse
{
    public ApplicationResponse()
    {        
    }

    public ApplicationResponse(Application app)
    {        
        Id = app.Id;
        Name = app.Name;
        CreatedAt = app.CreatedAt;
        UpdatedAt = app.UpdatedAt;
    }

    public int Id { get; set; }
    
    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}
