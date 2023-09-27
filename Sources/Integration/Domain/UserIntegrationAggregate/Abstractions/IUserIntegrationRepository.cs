using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Specifications;

namespace MlcAccounting.Integration.Domain.UserIntegrationAggregate.Abstractions;

public interface IUserIntegrationRepository
{
    Task<IEnumerable<UserIntegration>> GetAllAsync(UserIntegrationSpecification specification);

    Task<IEnumerable<UserIntegration>> GetAllAsync(Guid packageId);

    Task<UserIntegration?> GetAsync(Guid id);

    Task CreateAsync(UserIntegration userIntegration);

    Task UpdateAsync(UserIntegration userIntegration);
}