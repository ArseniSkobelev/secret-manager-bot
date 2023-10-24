using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretManagerBot.Domain.Handlers;

public interface IInteractionHandler<T>
{
    Task<bool> HandleInteractionAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}