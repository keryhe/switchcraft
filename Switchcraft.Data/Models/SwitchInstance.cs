using System;

namespace Switchcraft.Data.Models;

public class SwitchInstance
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public int? EnvironmentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Switch? Switch { get; set; }
    public virtual Environment? Environment { get; set; }
}
