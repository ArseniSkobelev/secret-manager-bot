using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretManagerBot.Data.Configuration;
using SecretManagerBot.Domain.Services.Bot;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<BotService>();
builder.Services.Configure<DatabaseConfiguration>(configuration.GetSection("Database"));
builder.Services.Configure<SecurityConfiguration>(configuration.GetSection("Security"));

using IHost host = builder.Build();

await host.RunAsync();