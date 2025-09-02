using System;
using Switchcraft.Client.Clients;
using Switchcraft.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Switchcraft.Client.Services.Default;
using Switchcraft.Client.Workers;

namespace Switchcraft.Client.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwitchcraft(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        
        services
            .AddTransient<ISwitchService, SwitchService>()
            .AddSingleton<ILocalCache, LocalCache>()
            .AddSingleton<ICacheSignal, CacheSignal>()
            .AddTransient<ISwitch, Switch>();


        services.AddHttpClient<ISwitchClient, SwitchClient>((client) =>
        {
            var baseAddress = configuration.GetValue<string>("Switchcraft:BaseAddress");
            if (!string.IsNullOrEmpty(baseAddress))
            {
                client.BaseAddress = new Uri(baseAddress);
            }
            else
            {
                throw new NullReferenceException("Switchcraft:BaseAddress is empty");
            }
        });
        
        services.AddHostedService<CacheWorker>();

        return services;
    } 
}
