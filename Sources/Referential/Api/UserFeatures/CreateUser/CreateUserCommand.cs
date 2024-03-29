﻿using MediatR;

namespace MlcAccounting.Referential.Api.UserFeatures.CreateUser;

public class CreateUserCommand : IRequest<Guid?>
{
    public string? Name { get; set; }

    public string? Password { get; set; }
}