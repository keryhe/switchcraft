using System;
using Microsoft.EntityFrameworkCore;

namespace Switchcraft.Server.Web.Data;

public class SwitchcraftDbContext : Switchcraft.Data.SwitchcraftDbContext<SwitchcraftDbContext>
{
    public SwitchcraftDbContext(DbContextOptions<SwitchcraftDbContext> options)
        : base(options)
    {
    }
}
