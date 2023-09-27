using MediatR;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate;

namespace MlcAccounting.Integration.Api.UserIntegrationFeatures.SubmitUserIntegration;

public class SubmitUserIntegrationHandler : IRequestHandler<SubmitUserIntegrationCommand, Guid>
{
    private readonly UserIntegrationService _service;

    public SubmitUserIntegrationHandler(UserIntegrationService service)
    {
        _service = service;
    }

    public async Task<Guid> Handle(SubmitUserIntegrationCommand request, CancellationToken cancellationToken) =>
        await _service.SubmitAsync(request.ToEntity());
}