using MlcAccounting.Common.Enums;
using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Domain.UserAggregate.Entities;
using MlcAccounting.Domain.UserAggregate.Specifications;

namespace MlcAccounting.Tests.Common.InMemories.Repositories;

public class UserInMemoryRepository : IUserRepository
{
    public static List<User> Data { get; set; } = new();

    public Task<IEnumerable<User>> GetAllAsync(UserSpecification specification)
    {
        var result = Data.Where(specification.Filter.Compile());

        result = specification.OrderBy switch
        {
            OrderType.Ascending => result.OrderBy(specification.Sort.Compile()),
            OrderType.Descending => result.OrderByDescending(specification.Sort.Compile()),
            _ => result.OrderBy(specification.Sort.Compile())
        };

        result = result
            .Skip((specification.PageIndex - 1) * specification.PageSize)
            .Take(specification.PageSize);

        return Task.FromResult(result);
    }

    public Task<IEnumerable<User>> GetAllAsync(string name) =>
        Task.FromResult(Data.Where(_ => _.Name == name));

    public Task<User?> GetAsync(Guid id) =>
        Task.FromResult(Data.FirstOrDefault(_ => _.Id == id));

    public Task CreateAsync(User user)
    {
        Data.Add(user);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        var current = Data.First(_ => _.Id == user.Id);

        Data[Data.IndexOf(current)] = user;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var current = Data.First(user => user.Id == id);

        Data.Remove(current);

        return Task.CompletedTask;
    }
}