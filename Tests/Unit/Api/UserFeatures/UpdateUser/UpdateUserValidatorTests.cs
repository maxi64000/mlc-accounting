using FluentValidation.TestHelper;
using MlcAccounting.Api.UserFeatures.UpdateUser;
using Xunit;

namespace MlcAccounting.Api.Tests.Unit.UserFeatures.UpdateUser;

public class UpdateUserValidatorTests
{
    private readonly UpdateUserValidator _validator;

    public UpdateUserValidatorTests()
    {
        _validator = new UpdateUserValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Have_Error_When_Null_Or_Empty_Name(string name)
    {
        // Act
        var actual = _validator.TestValidate(new UpdateUserCommand
        {
            Name = name,
            Password = "password"
        });

        // Arrange
        actual.ShouldHaveValidationErrorFor(_ => _.Name).WithErrorMessage("This field is mandatory.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Have_Error_When_Null_Or_Empty_Password(string password)
    {
        // Act
        var actual = _validator.TestValidate(new UpdateUserCommand
        {
            Name = "name",
            Password = password
        });

        // Arrange
        actual.ShouldHaveValidationErrorFor(_ => _.Password).WithErrorMessage("This field is mandatory.");
    }
}