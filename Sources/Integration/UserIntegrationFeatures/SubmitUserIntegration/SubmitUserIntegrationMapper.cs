using MlcAccounting.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.UserIntegrationFeatures.SubmitUserIntegration;

public static class SubmitUserIntegrationMapper
{
    public static UserIntegration ToEntity(this SubmitUserIntegrationCommand command) => new()
    {
        Name = command.Name,
        Password = command.Password
    };
}