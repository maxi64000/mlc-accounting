using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Abstractions;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Entities;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Specifications;

namespace MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate;

public class UserPackageIntegrationService
{
    private readonly IUserPackageIntegrationRepository _repository;

    private readonly IUserPackageIntegrationProducer _producer;

    public UserPackageIntegrationService(IUserPackageIntegrationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserPackageIntegration>> GetAllAsync(UserPackageIntegrationSpecification specification) => await _repository.GetAllAsync(specification);

    public async Task<UserPackageIntegration?> GetAsync(Guid id) => await _repository.GetAsync(id);

    public async Task<Guid> SubmitAsync(UserPackageIntegration userPackageIntegration)
    {
        await _repository.CreateAsync(userPackageIntegration);

        await _producer.ProduceAsync(userPackageIntegration);

        return userPackageIntegration.Id;
    }
}