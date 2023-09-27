using Microsoft.Extensions.Options;
using MlcAccounting.Common.Enums;
using MlcAccounting.Common.Mongo;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Abstractions;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Entities;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Specifications;
using MongoDB.Driver;

namespace MlcAccounting.Integration.Infrastructure.Repositories;

public class UserPackageIntegrationMongoRepository : IUserPackageIntegrationRepository
{
    private readonly IMongoCollection<UserPackageIntegration> _collection;

    public UserPackageIntegrationMongoRepository(IOptions<UserPackageIntegrationMongoRepositoryOptions> options)
    {
        _collection = new MongoClient(options.Value.ConnectionString)
            .GetDatabase(options.Value.Database)
            .GetCollection<UserPackageIntegration>(options.Value.Collection);
    }

    public async Task<IEnumerable<UserPackageIntegration>> GetAllAsync(UserPackageIntegrationSpecification specification)
    {
        var result = _collection
            .Find(specification.Filter)
            .Limit(specification.PageSize)
            .Skip((specification.PageIndex - 1) * specification.PageSize);

        result = specification.OrderBy switch
        {
            OrderType.Ascending => result.Sort(new SortDefinitionBuilder<UserPackageIntegration>().Ascending(specification.Sort)),
            OrderType.Descending => result.Sort(new SortDefinitionBuilder<UserPackageIntegration>().Descending(specification.Sort)),
            _ => result.Sort(new SortDefinitionBuilder<UserPackageIntegration>().Ascending(specification.Sort))
        };

        return await result.ToListAsync();
    }

    public async Task<UserPackageIntegration?> GetAsync(Guid id) =>
        await _collection.Find(_ => _.Id == id).SingleOrDefaultAsync();

    public async Task CreateAsync(UserPackageIntegration userPackageIntegration) =>
        await _collection.InsertOneAsync(userPackageIntegration);

    public async Task UpdateAsync(UserPackageIntegration userPackageIntegration) =>
        await _collection.ReplaceOneAsync(_ => _.Id == userPackageIntegration.Id, userPackageIntegration);
}

public class UserPackageIntegrationMongoRepositoryOptions : MongoOptions { }