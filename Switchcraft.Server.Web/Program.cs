using System.Text.Json;
using Switchcraft.Server.DependencyInjection;
using Switchcraft.Data.DependencyInjection;
using Switchcraft.Server.Web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("Switchcraft");
if (string.IsNullOrEmpty(connectionString))
{
    throw new NullReferenceException("ConnectionStrings:Switchcraft is empty");
}

builder.Services.AddSwitchcraftStore<SwitchcraftDbContext>(options =>
{
    options.ConfigureDbContext = builder => builder.UseMySQL(connectionString);
});

builder.Services.AddSwitchcraftApi(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
