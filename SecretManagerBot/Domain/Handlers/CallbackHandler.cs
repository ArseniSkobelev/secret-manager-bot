using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretManagerBot.Domain.Handlers;

public class CallbackHandler : IInteractionHandler<CallbackHandler>
{
    public async Task<bool> HandleInteractionAsync(ITelegramBotClient botClient, Update update, CancellationToken
        cancellationToken)
    {
        if (update.CallbackQuery == null) return false;

        return true;
    }
}