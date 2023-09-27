using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Api.UserIntegrationFeatures.SubmitUserIntegration;

public static class SubmitUserIntegrationMapper
{
    public static UserIntegration ToEntity(this SubmitUserIntegrationCommand command) => new()
    {
        Name = command.Name,
        Password = command.Password
    };
}