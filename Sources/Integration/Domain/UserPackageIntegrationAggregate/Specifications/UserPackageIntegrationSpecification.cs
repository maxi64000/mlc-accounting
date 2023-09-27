using MlcAccounting.Common.Enums;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Entities;
using System.Linq.Expressions;

namespace MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Specifications;

public class UserPackageIntegrationSpecification
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 25;

    public OrderType OrderBy { get; set; } = OrderType.Ascending;

    public Expression<Func<UserPackageIntegration, bool>> Filter { get; set; } = _ => true;

    public Expression<Func<UserPackageIntegration, object>> Sort { get; set; } = _ => _.Id;
}