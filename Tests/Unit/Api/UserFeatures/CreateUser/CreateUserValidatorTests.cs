using FluentValidation.TestHelper;
using MlcAccounting.Api.UserFeatures.CreateUser;
using Xunit;

namespace MlcAccounting.Api.Tests.Unit.UserFeatures.CreateUser;

public class CreateUserValidatorTests
{
    private readonly CreateUserValidator _validator;

    public CreateUserValidatorTests()
    {
        _validator = new CreateUserValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Should_Have_Error_When_Null_Or_Empty_Name(string name)
    {
        // Act
        var actual = _validator.TestValidate(new CreateUserCommand
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
        var actual = _validator.TestValidate(new CreateUserCommand
        {
            Name = "name",
            Password = password
        });

        // Arrange
        actual.ShouldHaveValidationErrorFor(_ => _.Password).WithErrorMessage("This field is mandatory.");
    }
}