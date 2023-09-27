using FluentValidation;

namespace MlcAccounting.Referential.Api.UserFeatures.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(_ => _.Name)
            .NotEmpty()
            .WithMessage("This field is mandatory.");

        RuleFor(_ => _.Password)
            .NotEmpty()
            .WithMessage("This field is mandatory.");
    }
}