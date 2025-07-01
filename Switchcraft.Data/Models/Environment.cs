using System;

namespace Switchcraft.Data.Models;

public class Environment
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<SwitchInstance> SwitchInstances { get; } = new List<SwitchInstance>();
}
