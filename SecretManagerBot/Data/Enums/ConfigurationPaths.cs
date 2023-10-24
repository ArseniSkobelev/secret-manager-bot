namespace SecretManagerBot.Data.Enums;

public static class ConfigurationPaths
{
    public static string TelegramApiToken { get; set; } = "Telegram:ApiToken";
    public static string DatabaseConnectionString { get; set; } = "Database:ConnectionString";
    public static string DatabaseName { get; set; } = "Database:Name";
}