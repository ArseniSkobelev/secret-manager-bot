using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using SecretManagerBot.Data.Configuration;
using SecretManagerBot.Data.Enums;
using SecretManagerBot.Domain.Handlers;
using SecretManagerBot.Domain.Services.Bot;
using SecretManagerBot.Domain.Services.User;
using Telegram.Bot;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<DatabaseConfiguration>(configuration.GetSection("Database"));
builder.Services.Configure<SecurityConfiguration>(configuration.GetSection("Security"));
builder.Services.Configure<TelegramConfiguration>(configuration.GetSection("Telegram"));

builder.Services.AddHostedService<BotService>();

builder.Services.AddScoped<IInteractionHandler<MessageHandler>, MessageHandler>();
builder.Services.AddScoped<IInteractionHandler<CommandHandler>, CommandHandler>();
builder.Services.AddScoped<IInteractionHandler<CallbackHandler>, CallbackHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBotHandler, BotHandler>();
builder.Services.AddSingleton(new TelegramBotClient(builder.Configuration[ConfigurationPaths.TelegramApiToken]!));
builder.Services.AddSingleton(new MongoClient(builder.Configuration[ConfigurationPaths.DatabaseConnectionString]!)
    .GetDatabase(builder.Configuration[ConfigurationPaths.DatabaseName]!));

using IHost host = builder.Build();

await host.RunAsync();