using MediatR;
using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Api.UserFeatures.GetUser;

public class GetUserQuery : IRequest<User?>
{
    public Guid Id { get; set; }
}