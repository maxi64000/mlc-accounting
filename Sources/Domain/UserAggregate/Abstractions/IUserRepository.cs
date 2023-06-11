using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Domain.UserAggregate.Abstractions;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id);
}