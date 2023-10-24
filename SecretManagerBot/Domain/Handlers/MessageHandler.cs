using Telegram.Bot;
using Telegram.Bot.Types;

namespace SecretManagerBot.Domain.Handlers;

public class MessageHandler : IInteractionHandler<MessageHandler>
{
    public async Task<bool> HandleInteractionAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(update.Message.From.Id, "Bip bap bop 🤖");
        return true;
    }
}