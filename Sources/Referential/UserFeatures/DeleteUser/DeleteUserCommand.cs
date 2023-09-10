using MediatR;

namespace MlcAccounting.Referential.UserFeatures.DeleteUser;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}