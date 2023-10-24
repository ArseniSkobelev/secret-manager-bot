using MongoDB.Bson.Serialization.Attributes;

namespace SecretManagerBot.Data.RootDocuments;

public class UserRootDocument : RootDocument
{
    public UserRootDocument(string username, long chatId)
    {
        Username = username;
        ChatId = chatId;
    }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("chat_id")]
    public long ChatId { get; set; }
}