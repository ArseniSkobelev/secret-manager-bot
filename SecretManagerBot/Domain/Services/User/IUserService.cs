using SecretManagerBot.Data.DTOs;

namespace SecretManagerBot.Domain.Services.User;

public interface IUserService
{
    Task<ServiceResponse> BootstrapUserAsync(Telegram.Bot.Types.User user);
}