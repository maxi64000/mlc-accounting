using MediatR;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Api.UserIntegrationFeatures.GetAllUserIntegrations;

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