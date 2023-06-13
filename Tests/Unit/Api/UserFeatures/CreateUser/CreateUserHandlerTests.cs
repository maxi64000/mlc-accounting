using FluentAssertions;
using MlcAccounting.Api.UserFeatures.CreateUser;
using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MlcAccounting.Api.Tests.Unit.UserFeatures.CreateUser;

public class CreateUserHandlerTests
{
    private readonly Mock<IUserService> _userService;

    private readonly CreateUserHandler _handler;

    public CreateUserHandlerTests()
    {
        _userService = new Mock<IUserService>();

        _handler = new CreateUserHandler(_userService.Object);
    }

    [Fact]
    public async Task Should_Create_User_Successfully()
    {
        // Arrange
        var expected = Guid.NewGuid();

        _userService
            .Setup(_ => _.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _handler.Handle(new CreateUserCommand(), CancellationToken.None);

        // Assert
        actual.Should().Be(expected);
    }
}