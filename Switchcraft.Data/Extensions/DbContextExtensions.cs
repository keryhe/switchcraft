using System;
using Microsoft.EntityFrameworkCore;

namespace Switchcraft.Data.Extensions;

public static class DbContextExtensions
{
    public static void UpdateEntityTimestamps(this DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Property("CreatedAt").CurrentValue == null || (entry.Property("CreatedAt").CurrentValue is DateTime dateTime && dateTime == default))
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
