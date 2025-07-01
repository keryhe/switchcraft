using System;
using Microsoft.EntityFrameworkCore;
using Switchcraft.Data.Models;

namespace Switchcraft.Data;

public interface ISwitchcraftDbContext
{
    DbSet<Models.Environment> Environments { get; set; }

    DbSet<Application> Applications { get; set; }

    DbSet<Switch> Switches { get; set; }

    DbSet<SwitchInstance> SwitchInstances { get; set; }

    int SaveChanges();

    int SaveChanges(bool acceptAllChangesOnSuccess);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

     Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
}
