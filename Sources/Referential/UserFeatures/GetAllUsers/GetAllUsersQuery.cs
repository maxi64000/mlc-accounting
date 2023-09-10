using MediatR;
using MlcAccounting.Common.Enums;
using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.UserFeatures.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<User>>
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 25;

    public string? SortBy { get; set; }

    public OrderType? OrderBy { get; set; }

    public string? Name { get; set; }
}