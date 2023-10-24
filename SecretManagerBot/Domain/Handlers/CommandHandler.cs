using SecretManagerBot.Data.DTOs;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SecretManagerBot.Domain.Handlers;

public class CommandHandler : IInteractionHandler<CommandHandler>
{
    public async Task<bool> HandleInteractionAsync(ITelegramBotClient botClient, Update update, CancellationToken 
        cancellationToken)
    {
        var messageText = update.Message!.Text;
        if (string.IsNullOrWhiteSpace(messageText)) return false;
        if (!messageText.Contains('/')) return false;

        ServiceResponse response = messageText.Replace("/", "") switch
        {
            "start" => StartCommandHandler(botClient, update, cancellationToken).Result,
            _ => throw new ArgumentOutOfRangeException()
        };

        return response.IsSuccess;
    }

    private async Task<ServiceResponse> StartCommandHandler(ITelegramBotClient botClient, Update update, CancellationToken
        cancellationToken)
    {
        InlineKeyboardMarkup inlineKeyboard = new(new[]
        {
            // first row
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "1.1", callbackData: "11"),
                InlineKeyboardButton.WithCallbackData(text: "1.2", callbackData: "12"),
            },
            // second row
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "2.1", callbackData: "21"),
                InlineKeyboardButton.WithCallbackData(text: "2.2", callbackData: "22"),
            },
        });

        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: update.Message.From.Id,
            text: "A message with an inline keyboard markup",
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);

        return new ServiceResponse(true);
    }
}