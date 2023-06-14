using MediatR;

namespace MlcAccounting.Api.UserFeatures.DeleteUser;

public class DeleteUserCommand: IRequest<bool>
{
    public Guid Id { get; set; }
}