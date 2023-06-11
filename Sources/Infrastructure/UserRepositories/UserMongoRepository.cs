using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Domain.UserAggregate.Entities;
using MongoDB.Driver;

namespace MlcAccounting.Infrastructure.UserRepositories;

public class UserMongoRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserMongoRepository(IMongoCollection<User> collection)
    {
        _collection = collection;
    }

    public async Task<User?> GetAsync(Guid id)
    {
        return await _collection.Find(user => user.Id == id).SingleOrDefaultAsync();
    }
}