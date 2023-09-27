using MlcAccounting.Common.Enums;
using MlcAccounting.Referential.Domain.UserAggregate.Entities;
using MlcAccounting.Referential.Domain.UserAggregate.Specifications;
using System.Linq.Expressions;

namespace MlcAccounting.Referential.Api.UserFeatures.GetAllUsers;

public static class GetAllUsersMapper
{
    public static UserSpecification ToSpecification(this GetAllUsersQuery query)
    {
        Expression<Func<User, bool>> filter = _ => true;

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            filter = user => user.Name == query.Name;
        }

        return new UserSpecification
        {
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Filter = filter,
            OrderBy = query.OrderBy ?? OrderType.Ascending,
            Sort = query.SortBy?.ToLower() switch
            {
                "name" => user => user.Name,
                "password" => user => user.Password,
                "createdat" => user => user.CreatedAt,
                "updatedat" => user => user.UpdatedAt!,
                _ => user => user.Id
            }
        };
    }
}