using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Domain.UserIntegrationAggregate.Abstractions;

public interface IUserIntegrationProducer
{
    Task ProduceAsync(UserIntegration message);
}