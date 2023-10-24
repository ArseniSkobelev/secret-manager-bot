using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecretManagerBot.Data.Configuration;
using SecretManagerBot.Data.DTOs;
using SecretManagerBot.Data.Enums;
using SecretManagerBot.Data.RootDocuments;

namespace SecretManagerBot.Domain.Services.User;

public class UserService : IUserService
{
    private readonly SecurityConfiguration _securityConfiguration;
    private readonly IMongoCollection<UserRootDocument> _userCollection;

    public UserService(IMongoDatabase mongoDatabase, IOptions<SecurityConfiguration> securityConfiguration)
    {
        _securityConfiguration = securityConfiguration.Value;
        _userCollection = mongoDatabase.GetCollection<UserRootDocument>(DatabaseCollections.Users);
    }

    public async Task<ServiceResponse> BootstrapUserAsync(Telegram.Bot.Types.User user)
    {
        var userFilter = Builders<UserRootDocument>.Filter.Eq(x => x.Username, user.Username);

        var maybeUser = await _userCollection.FindAsync(userFilter).ConfigureAwait(false);

        try
        {
            var userObject = maybeUser.First<UserRootDocument>();
            if (userObject != null) return new ServiceResponse(true);
        }
        catch (Exception _)
        {
            var newUser = new UserRootDocument(user.Username!, user.Id);
            await _userCollection.InsertOneAsync(newUser);

            return new ServiceResponse(true);
        }

        return new ServiceResponse(true);
    }
}