using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Switchcraft.Server.Controllers.Services;
using Switchcraft.Server.Controllers.Services.Default;

namespace Switchcraft.Server.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwitchcraftApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddTransient<ISwitchService, SwitchService>()
            .AddTransient<ISwitchInstanceService, SwitchInstanceService>()
            .AddTransient<IEnvironmentService, EnvironmentService>()
            .AddTransient<IApplicationService, ApplicationService>();

        return services;
    } 
}
