using MediatR;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Api.UserIntegrationFeatures.GetUserIntegration;

public class GetUserIntegrationQuery : IRequest<UserIntegration?>
{
    public Guid Id { get; set; }
}