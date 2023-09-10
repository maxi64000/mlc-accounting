using MediatR;
using MlcAccounting.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.UserIntegrationFeatures.GetUserIntegration;

public class GetUserIntegrationQuery : IRequest<UserIntegration?>
{
    public Guid Id { get; set; }
}