using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.UserFeatures.CreateUser;

public static class CreateUserMapper
{
    public static User ToEntity(this CreateUserCommand command) => new(command.Name!, command.Password!);
}