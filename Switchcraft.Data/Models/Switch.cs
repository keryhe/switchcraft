using System;

namespace Switchcraft.Data.Models;

public class Switch
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required int? ApplicationId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Application? Application { get; set; }

    public virtual ICollection<SwitchInstance> SwitchInstances { get; } = new List<SwitchInstance>();

}
