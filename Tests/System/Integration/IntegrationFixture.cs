using MongoDB.Driver;
using User = MlcAccounting.Referential.Domain.UserAggregate.Entities.User;
using UserIntegration = MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities.UserIntegration;

namespace MlcAccounting.Tests.System.Integration;

public class IntegrationFixture
{
    public readonly IMongoCollection<User> UserCollection;

    public readonly IMongoCollection<UserIntegration> UserIntegrationCollection;

    public IntegrationFixture()
    {
        UserCollection = new MongoClient("mongodb://root:example@localhost:27017?authSource=admin")
            .GetDatabase("mlc-accounting")
            .GetCollection<User>("user");

        UserIntegrationCollection = new MongoClient("mongodb://root:example@localhost:27017?authSource=admin")
            .GetDatabase("mlc-accounting")
            .GetCollection<UserIntegration>("user-integration");
    }
}