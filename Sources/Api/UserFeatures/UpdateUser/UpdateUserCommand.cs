using MediatR;

namespace MlcAccounting.Api.UserFeatures.UpdateUser;

public class UpdateUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }
}