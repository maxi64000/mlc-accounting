using MlcAccounting.Referential.Domain.UserAggregate.Entities;
using MlcAccounting.Referential.Domain.UserAggregate.Specifications;

namespace MlcAccounting.Referential.Domain.UserAggregate.Abstractions;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(UserSpecification specification);

    Task<IEnumerable<User>> GetAllAsync(string name);

    Task<User?> GetAsync(Guid id);

    Task CreateAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(Guid id);
}