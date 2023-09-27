using MediatR;

namespace MlcAccounting.Referential.Api.UserFeatures.DeleteUser;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}