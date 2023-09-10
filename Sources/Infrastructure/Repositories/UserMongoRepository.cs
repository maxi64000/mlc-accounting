using Microsoft.Extensions.Options;
using MlcAccounting.Common.Enums;
using MlcAccounting.Common.Mongo;
using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Domain.UserAggregate.Entities;
using MlcAccounting.Domain.UserAggregate.Specifications;
using MongoDB.Driver;

namespace MlcAccounting.Infrastructure.Repositories;

public class UserMongoRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserMongoRepository(IOptions<UserMongoRepositoryOptions> options)
    {
        _collection = new MongoClient(options.Value.ConnectionString)
            .GetDatabase(options.Value.Database)
            .GetCollection<User>(options.Value.Collection);
    }

    public async Task<IEnumerable<User>> GetAllAsync(UserSpecification specification)
    {
        var result = _collection
            .Find(specification.Filter)
            .Limit(specification.PageSize)
            .Skip((specification.PageIndex - 1) * specification.PageSize);

        result = specification.OrderBy switch
        {
            OrderType.Ascending => result.Sort(new SortDefinitionBuilder<User>().Ascending(specification.Sort)),
            OrderType.Descending => result.Sort(new SortDefinitionBuilder<User>().Descending(specification.Sort)),
            _ => result.Sort(new SortDefinitionBuilder<User>().Ascending(specification.Sort))
        };

        return await result.ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync(string name) =>
        await _collection.Find(_ => _.Name == name).ToListAsync();

    public async Task<User?> GetAsync(Guid id) =>
        await _collection.Find(_ => _.Id == id).SingleOrDefaultAsync();

    public async Task CreateAsync(User user) =>
        await _collection.InsertOneAsync(user);

    public async Task UpdateAsync(User user) =>
        await _collection.ReplaceOneAsync(_ => _.Id == user.Id, user);

    public async Task DeleteAsync(Guid id) =>
        await _collection.DeleteOneAsync(_ => _.Id == id);
}

public class UserMongoRepositoryOptions : MongoOptions { }