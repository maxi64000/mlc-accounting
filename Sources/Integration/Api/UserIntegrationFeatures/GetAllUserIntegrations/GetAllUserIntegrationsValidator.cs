using FluentValidation;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Api.UserIntegrationFeatures.GetAllUserIntegrations;

public class GetAllUserIntegrationsValidator : AbstractValidator<GetAllUserIntegrationsQuery>
{
    public GetAllUserIntegrationsValidator()
    {
        var properties = typeof(UserIntegration).GetProperties().Select(property => property.Name.ToLower()).ToList();

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