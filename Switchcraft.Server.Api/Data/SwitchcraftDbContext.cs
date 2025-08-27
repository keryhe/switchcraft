using System;
using Microsoft.EntityFrameworkCore;

namespace Switchcraft.Server.Api.Data;

public class SwitchcraftDbContext : Switchcraft.Data.SwitchcraftDbContext<SwitchcraftDbContext>
{
    public SwitchcraftDbContext(DbContextOptions<SwitchcraftDbContext> options)
        : base(options)
    {
    }
}
