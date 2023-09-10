using MlcAccounting.Domain.UserIntegrationAggregate.Entities;
using MlcAccounting.Domain.UserIntegrationAggregate.Specifications;

namespace MlcAccounting.Domain.UserIntegrationAggregate.Abstractions;

public interface IUserIntegrationRepository
{
    Task<IEnumerable<UserIntegration>> GetAllAsync(UserIntegrationSpecification specification);

    Task<UserIntegration?> GetAsync(Guid id);

    Task CreateAsync(UserIntegration user);

    Task UpdateAsync(UserIntegration user);
}