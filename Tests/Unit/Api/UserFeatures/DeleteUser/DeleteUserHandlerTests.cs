using FluentAssertions;
using MlcAccounting.Api.UserFeatures.DeleteUser;
using MlcAccounting.Domain.UserAggregate;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MlcAccounting.Api.Tests.Unit.UserFeatures.DeleteUser;

public class DeleteUserHandlerTests
{
    private readonly Mock<IUserService> _userService;

    private readonly DeleteUserHandler _handler;

    public DeleteUserHandlerTests()
    {
        _userService = new Mock<IUserService>();

        _handler = new DeleteUserHandler(_userService.Object);
    }

    [Fact]
    public async Task Should_Delete_User_Successfully()
    {
        // Arrange
        _userService
            .Setup(_ => _.DeleteAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        // Act
        var actual = await _handler.Handle(new DeleteUserCommand(), CancellationToken.None);

        // Assert
        actual.Should().BeTrue();
    }
}