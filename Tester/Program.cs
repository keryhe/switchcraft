using Switchcraft.Client.DependencyInjection;
using Tester;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSwitchcraft(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
