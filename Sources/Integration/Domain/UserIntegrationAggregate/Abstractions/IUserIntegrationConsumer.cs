using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Domain.UserIntegrationAggregate.Abstractions;

public interface IUserIntegrationConsumer
{
    public UserIntegration Consume();

    public void Commit();
}