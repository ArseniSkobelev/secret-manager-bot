namespace SecretManagerBot.Data.Exceptions;

public abstract class ConfigurationExceptions
{
    [Serializable]
    public class ConfigurationMissingException : Exception
    { }
}