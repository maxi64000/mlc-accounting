using MlcAccounting.Referential.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.Api.UserFeatures.CreateUser;

public static class CreateUserMapper
{
    public static User ToEntity(this CreateUserCommand command) => new(command.Name!, command.Password!);
}