using MediatR;
using MlcAccounting.Domain.UserIntegrationAggregate;
using MlcAccounting.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.UserIntegrationFeatures.GetAllUserIntegrations;

public class GetAllUserIntegrationsHandler : IRequestHandler<GetAllUserIntegrationsQuery, IEnumerable<UserIntegration>>
{
    private readonly UserIntegrationService _service;

    public GetAllUserIntegrationsHandler(UserIntegrationService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<UserIntegration>> Handle(GetAllUserIntegrationsQuery request, CancellationToken cancellationToken) =>
        await _service.GetAllAsync(request.ToSpecification());
}