using Microsoft.Extensions.Hosting;
using SecretManagerBot.Domain.Handlers;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace SecretManagerBot.Domain.Services.Bot;

public class BotService : IHostedService
{
    private readonly TelegramBotClient _botClient;
    private readonly IInteractionHandler<MessageHandler> _messageHandler;
    private readonly IBotHandler _botHandler;

    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    private readonly ReceiverOptions _receiverOptions = new ()
    {
        AllowedUpdates = Array.Empty<UpdateType>()
    };

    public BotService(TelegramBotClient botClient, IInteractionHandler<MessageHandler> messageHandler, IBotHandler botHandler)
    {
        _botClient = botClient;
        _messageHandler = messageHandler;
        _botHandler = botHandler;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _botClient.StartReceiving(
            updateHandler: _botHandler.HandleUpdateAsync,
            pollingErrorHandler: _botHandler.HandlePollingErrorAsync,
            receiverOptions: _receiverOptions,
            cancellationToken: _cts.Token
        );

        Console.WriteLine("Bootstrapping Telegram API connection..");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}