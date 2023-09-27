using MlcAccounting.Common.Enums;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;
using System.Linq.Expressions;

namespace MlcAccounting.Integration.Domain.UserIntegrationAggregate.Specifications;

public class UserIntegrationSpecification
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 25;

    public OrderType OrderBy { get; set; } = OrderType.Ascending;

    public Expression<Func<UserIntegration, bool>> Filter { get; set; } = _ => true;

    public Expression<Func<UserIntegration, object>> Sort { get; set; } = _ => _.Id;
}