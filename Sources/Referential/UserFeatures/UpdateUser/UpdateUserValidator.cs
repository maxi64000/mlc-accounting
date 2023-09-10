using FluentValidation;

namespace MlcAccounting.Referential.UserFeatures.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("This field is mandatory.");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("This field is mandatory.");
    }
}