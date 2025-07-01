using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Switchcraft.Data.DependencyInjection.Options;

namespace Switchcraft.Data.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwitchcraftStore(this IServiceCollection services, Action<SwitchcraftStoreOptions>? storeOptionsAction = null)
    {
        return services.AddSwitchcraftStore<SwitchcraftDbContext>(storeOptionsAction);
    }

    public static IServiceCollection AddSwitchcraftStore<TContext>(this IServiceCollection services, Action<SwitchcraftStoreOptions>? storeOptionsAction = null)
        where TContext : DbContext, ISwitchcraftDbContext
    {
        var options = new SwitchcraftStoreOptions();
        services.AddSingleton(options);
        storeOptionsAction?.Invoke(options);

        services.AddDbContext<TContext>(dbContextBuilder =>
        {
            options.ConfigureDbContext?.Invoke(dbContextBuilder);
        });

        services.AddScoped<ISwitchcraftDbContext>(s => s.GetRequiredService<TContext>());

        return services;
    }
}
