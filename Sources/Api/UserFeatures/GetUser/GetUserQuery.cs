using MediatR;

namespace MlcAccounting.Api.UserFeatures.GetUser;

public class GetUserQuery : IRequest<Domain.UserAggregate.Entities.User?>
{
    public Guid Id { get; set; }
}