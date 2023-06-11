using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Infrastructure.UserRepositories;

public class UserInMemoryRepository : IUserRepository
{
    public static List<User> Data { get; set; } = new();

    public Task<User?> GetAsync(Guid id) =>
        Task.FromResult(Data.FirstOrDefault(_ => _.Id == id));
}