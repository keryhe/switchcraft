using System;

namespace Switchcraft.Data.Models;

public class Application
{
    public int Id { get; set; }
    
    public required string Name { get; set; }

    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Switch> Switches { get; } = new List<Switch>();
}
