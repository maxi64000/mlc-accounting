using MlcAccounting.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Domain.UserIntegrationAggregate.Abstractions;

public interface IUserIntegrationProducer
{
    Task ProduceAsync(UserIntegration message);
}