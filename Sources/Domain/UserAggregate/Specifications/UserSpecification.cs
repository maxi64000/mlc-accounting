using MlcAccounting.Common.Enums;
using MlcAccounting.Domain.UserAggregate.Entities;
using System.Linq.Expressions;

namespace MlcAccounting.Domain.UserAggregate.Specifications;

public class UserSpecification
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 25;

    public OrderType OrderBy { get; set; } = OrderType.Ascending;

    public Expression<Func<User, bool>> Filter { get; set; } = _ => true;

    public Expression<Func<User, object>> Sort { get; set; } = _ => _.Id;
}