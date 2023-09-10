using FluentValidation;
using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.UserFeatures.GetAllUsers;

public class GetAllUsersValidator : AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersValidator()
    {
        var properties = typeof(User).GetProperties().Select(property => property.Name.ToLower()).ToList();

        RuleFor(query => query.SortBy)
            .Must(sortBy => string.IsNullOrWhiteSpace(sortBy) || properties.Contains(sortBy.ToLower()))
            .WithMessage("The value '{PropertyValue}' is not valid.");

        RuleFor(query => query.PageIndex)
            .GreaterThan(0)
            .WithMessage("This field must be greater than 0.");

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .WithMessage("This field must be greater than 0.");
    }
}