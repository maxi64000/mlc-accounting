using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Domain.UserAggregate.Abstractions;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(string name);

    Task<User?> GetAsync(Guid id);

    Task CreateAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(Guid id);
}