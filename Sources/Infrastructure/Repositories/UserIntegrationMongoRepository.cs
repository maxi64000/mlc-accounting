using Microsoft.Extensions.Options;
using MlcAccounting.Common.Enums;
using MlcAccounting.Common.Mongo;
using MlcAccounting.Domain.UserIntegrationAggregate.Abstractions;
using MlcAccounting.Domain.UserIntegrationAggregate.Entities;
using MlcAccounting.Domain.UserIntegrationAggregate.Specifications;
using MongoDB.Driver;

namespace MlcAccounting.Infrastructure.Repositories;

public class UserIntegrationMongoRepository : IUserIntegrationRepository
{
    private readonly IMongoCollection<UserIntegration> _collection;

    public UserIntegrationMongoRepository(IOptions<UserIntegrationMongoRepositoryOptions> options)
    {
        _collection = new MongoClient(options.Value.ConnectionString)
            .GetDatabase(options.Value.Database)
            .GetCollection<UserIntegration>(options.Value.Collection);
    }

    public async Task<IEnumerable<UserIntegration>> GetAllAsync(UserIntegrationSpecification specification)
    {
        var result = _collection
            .Find(specification.Filter)
            .Limit(specification.PageSize)
            .Skip((specification.PageIndex - 1) * specification.PageSize);

        result = specification.OrderBy switch
        {
            OrderType.Ascending => result.Sort(new SortDefinitionBuilder<UserIntegration>().Ascending(specification.Sort)),
            OrderType.Descending => result.Sort(new SortDefinitionBuilder<UserIntegration>().Descending(specification.Sort)),
            _ => result.Sort(new SortDefinitionBuilder<UserIntegration>().Ascending(specification.Sort))
        };

        return await result.ToListAsync();
    }

    public async Task<UserIntegration?> GetAsync(Guid id) =>
        await _collection.Find(_ => _.Id == id).SingleOrDefaultAsync();

    public async Task CreateAsync(UserIntegration userIntegration) =>
        await _collection.InsertOneAsync(userIntegration);

    public async Task UpdateAsync(UserIntegration userIntegration) =>
        await _collection.ReplaceOneAsync(_ => _.Id == userIntegration.Id, userIntegration);
}

public class UserIntegrationMongoRepositoryOptions : MongoOptions { }