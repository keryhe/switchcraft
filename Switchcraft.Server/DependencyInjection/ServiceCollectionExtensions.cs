using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Switchcraft.Server.Controllers.Hubs.Default;
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
            .AddTransient<IApplicationService, ApplicationService>()
            .AddScoped<INotificationService, NotificationService> ();

        services.AddSignalR();

        return services;
    }

    public static WebApplication MapSignalRClient(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/hubs/notification");
        return app;
    }
}
