using Microsoft.EntityFrameworkCore;
using Switchcraft.Data.Extensions;
using Switchcraft.Data.Models;

namespace Switchcraft.Data;

public class SwitchcraftDbContext : SwitchcraftDbContext<SwitchcraftDbContext>
{
    public SwitchcraftDbContext(DbContextOptions<SwitchcraftDbContext> options)
        : base(options)
        {
        }
}

public class SwitchcraftDbContext<TContext> : DbContext, ISwitchcraftDbContext
    where TContext : DbContext, ISwitchcraftDbContext
{
    public SwitchcraftDbContext(DbContextOptions<TContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Environment> Environments { get; set; }

    public DbSet<Application> Applications { get; set; }

    public DbSet<Switch> Switches { get; set; }

    public DbSet<SwitchInstance> SwitchInstances { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Models.Environment>()
            .HasIndex(e => e.Name)
            .IsUnique(true);

        builder.Entity<Application>()
            .HasIndex(a => a.Name)
            .IsUnique(true);

        builder.Entity<Switch>()
            .HasIndex(s => new { s.Name, s.ApplicationId })
            .IsUnique(true);

    }

    public override int SaveChanges()
    {
        this.UpdateEntityTimestamps();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.UpdateEntityTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.UpdateEntityTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        this.UpdateEntityTimestamps();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}


