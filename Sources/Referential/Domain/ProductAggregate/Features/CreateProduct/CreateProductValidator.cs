using FluentValidation;

namespace MlcAccounting.Referential.Domain.ProductAggregate.Features.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(_ => _.Name)
            .NotEmpty()
            .WithMessage("This field is mandatory.");
    }
}