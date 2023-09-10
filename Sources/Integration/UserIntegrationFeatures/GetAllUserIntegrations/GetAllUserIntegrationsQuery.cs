using MediatR;
using MlcAccounting.Common.Enums;
using MlcAccounting.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.UserIntegrationFeatures.GetAllUserIntegrations;

public class GetAllUserIntegrationsQuery : IRequest<IEnumerable<UserIntegration>>
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 25;

    public string? SortBy { get; set; }

    public OrderType? OrderBy { get; set; }

    public string? Name { get; set; }
}