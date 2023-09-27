using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Abstractions;

public interface IUserPackageIntegrationConsumer
{
    public UserPackageIntegration Consume();

    public void Commit();
}