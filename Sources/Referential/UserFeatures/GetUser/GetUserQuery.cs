using MediatR;
using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.UserFeatures.GetUser;

public class GetUserQuery : IRequest<User?>
{
    public Guid Id { get; set; }
}