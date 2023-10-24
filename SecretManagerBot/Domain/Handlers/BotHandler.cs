using SecretManagerBot.Domain.Services.User;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SecretManagerBot.Domain.Handlers;

public class BotHandler : IBotHandler
{
    private readonly IInteractionHandler<MessageHandler> _messageHandler;
    private readonly IInteractionHandler<CommandHandler> _commandHandler;
    private readonly IInteractionHandler<CallbackHandler> _callbackHandler;
    private readonly IUserService _userService;

    public BotHandler(IInteractionHandler<MessageHandler> messageHandler, IUserService userService, IInteractionHandler<CommandHandler> commandHandler, IInteractionHandler<CallbackHandler> callbackHandler)
    {
        _messageHandler = messageHandler;
        _userService = userService;
        _commandHandler = commandHandler;
        _callbackHandler = callbackHandler;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if(update.Type == UpdateType.Message)
            await _userService.BootstrapUserAsync(update.Message.From);

        if(update.Type == UpdateType.CallbackQuery)
            if (await _callbackHandler.HandleInteractionAsync(botClient, update, cancellationToken)) return;

        if(await _commandHandler.HandleInteractionAsync(botClient, update, cancellationToken)) return;

        await _messageHandler.HandleInteractionAsync(botClient, update, cancellationToken);
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken 
            cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}