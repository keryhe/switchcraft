using System;
using Microsoft.EntityFrameworkCore;

namespace Switchcraft.Data.DependencyInjection.Options;

public class SwitchcraftStoreOptions
{
    public Action<DbContextOptionsBuilder>? ConfigureDbContext { get; set; }
}
