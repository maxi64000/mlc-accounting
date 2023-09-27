using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Entities;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Specifications;

namespace MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Abstractions;

public interface IUserPackageIntegrationRepository
{
    Task<IEnumerable<UserPackageIntegration>> GetAllAsync(UserPackageIntegrationSpecification specification);

    Task<UserPackageIntegration?> GetAsync(Guid id);

    Task CreateAsync(UserPackageIntegration userPackageIntegration);

    Task UpdateAsync(UserPackageIntegration userPackageIntegration);
}