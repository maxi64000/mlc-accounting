using MlcAccounting.Common.Enums;
using MlcAccounting.Domain.UserIntegrationAggregate.Entities;
using MlcAccounting.Domain.UserIntegrationAggregate.Specifications;
using System.Linq.Expressions;

namespace MlcAccounting.Integration.UserIntegrationFeatures.GetAllUserIntegrations;

public static class GetAllUserIntegrationsMapper
{
    public static UserIntegrationSpecification ToSpecification(this GetAllUserIntegrationsQuery query)
    {
        Expression<Func<UserIntegration, bool>> filter = _ => true;

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            filter = user => user.Name == query.Name;
        }

        return new UserIntegrationSpecification
        {
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Filter = filter,
            OrderBy = query.OrderBy ?? OrderType.Ascending,
            Sort = query.SortBy?.ToLower() switch
            {
                "name" => user => user.Name!,
                "password" => user => user.Password!,
                "createdat" => user => user.CreatedAt,
                "updatedat" => user => user.UpdatedAt!,
                _ => user => user.Id
            }
        };
    }
}