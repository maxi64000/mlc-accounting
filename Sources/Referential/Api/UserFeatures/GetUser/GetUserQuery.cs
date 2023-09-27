using MediatR;
using MlcAccounting.Referential.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.Api.UserFeatures.GetUser;

public class GetUserQuery : IRequest<User?>
{
    public Guid Id { get; set; }
}