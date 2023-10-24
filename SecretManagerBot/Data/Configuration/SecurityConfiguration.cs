namespace SecretManagerBot.Data.Configuration;

public class SecurityConfiguration
{
    public string SymmetricKey { get; set; }
    public List<string> AllowedUsers { get; set; }
}