using MediatR;

namespace MlcAccounting.Integration.Api.UserIntegrationFeatures.SubmitUserIntegration;

public class SubmitUserIntegrationCommand : IRequest<Guid>
{
    public string? Name { get; set; }

    public string? Password { get; set; }
}