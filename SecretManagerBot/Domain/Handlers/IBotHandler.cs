using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretManagerBot.Domain.Handlers;

public interface IBotHandler
{
    Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);

    Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken);
}