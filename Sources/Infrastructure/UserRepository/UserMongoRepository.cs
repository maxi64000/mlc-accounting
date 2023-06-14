using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Domain.UserAggregate.Entities;
using MlcAccounting.Infrastructure.UserRepository.Dtos;
using MlcAccounting.Infrastructure.UserRepository.Mappers;
using MongoDB.Driver;

namespace MlcAccounting.Infrastructure.UserRepository;

public class UserMongoRepository : IUserRepository
{
    private readonly IMongoCollection<UserDto> _collection;

    public UserMongoRepository(IMongoCollection<UserDto> collection)
    {
        _collection = collection;
    }

    public async Task<IEnumerable<User>> GetAllAsync(string name) =>
        (await _collection.Find(_ => _.Name == name).ToListAsync()).ToEntity();

    public async Task<User?> GetAsync(Guid id) =>
        (await _collection.Find(_ => _.Id == id).SingleOrDefaultAsync())?.ToEntity();

    public async Task CreateAsync(User user) =>
        await _collection.InsertOneAsync(new UserDto(user));

    public async Task UpdateAsync(User user) =>
        await _collection.ReplaceOneAsync(_ => _.Id == user.Id, new UserDto(user));

    public async Task DeleteAsync(Guid id) =>
        await _collection.DeleteOneAsync(user => user.Id == id);
}