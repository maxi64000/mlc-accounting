using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Domain.UserIntegrationAggregate.Abstractions;

public interface IUserReferentialClient
{
    Task<IEnumerable<User>> GetAllAsync(string name);

    Task CreateAsync(User user);

    Task UpdateAsync(User user);
}