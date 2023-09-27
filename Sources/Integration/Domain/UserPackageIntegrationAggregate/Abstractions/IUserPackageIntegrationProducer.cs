using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Abstractions;

public interface IUserPackageIntegrationProducer
{
    Task ProduceAsync(UserPackageIntegration message);
}