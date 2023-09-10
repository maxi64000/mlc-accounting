using MlcAccounting.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Domain.UserIntegrationAggregate.Abstractions;

public interface IUserIntegrationConsumer
{
    public UserIntegration Consume();

    public void Commit();
}