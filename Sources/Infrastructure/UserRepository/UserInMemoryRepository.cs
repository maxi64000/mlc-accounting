using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Domain.UserAggregate.Entities;
using MlcAccounting.Infrastructure.UserRepository.Dtos;
using MlcAccounting.Infrastructure.UserRepository.Mappers;

namespace MlcAccounting.Infrastructure.UserRepository;

public class UserInMemoryRepository : IUserRepository
{
    public static List<UserDto> Data { get; set; } = new();

    public Task<IEnumerable<User>> GetAllAsync(string name) =>
        Task.FromResult(Data.Where(_ => _.Name == name).ToEntity());

    public Task<User?> GetAsync(Guid id) =>
        Task.FromResult(Data.FirstOrDefault(_ => _.Id == id)?.ToEntity());

    public Task CreateAsync(User user)
    {
        Data.Add(new UserDto(user));

        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        var current = Data.First(_ => _.Id == user.Id);

        Data[Data.IndexOf(current)] = new UserDto(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var current = Data.First(user => user.Id == id);

        Data.Remove(current);

        return Task.CompletedTask;
    }
}