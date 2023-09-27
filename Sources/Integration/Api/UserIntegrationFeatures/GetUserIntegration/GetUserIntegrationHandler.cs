using MediatR;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Api.UserIntegrationFeatures.GetUserIntegration;

public class GetUserIntegrationHandler : IRequestHandler<GetUserIntegrationQuery, UserIntegration?>
{
    private readonly UserIntegrationService _service;

    public GetUserIntegrationHandler(UserIntegrationService service)
    {
        _service = service;
    }

    public async Task<UserIntegration?> Handle(GetUserIntegrationQuery request, CancellationToken cancellationToken) =>
        await _service.GetAsync(request.Id);
}