using FluentAssertions;
using MlcAccounting.Api.UserFeatures.UpdateUser;
using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MlcAccounting.Api.Tests.Unit.UserFeatures.UpdateUser;

public class UpdateUserHandlerTests
{
    private readonly Mock<IUserService> _userService;

    private readonly UpdateUserHandler _handler;

    public UpdateUserHandlerTests()
    {
        _userService = new Mock<IUserService>();

        _handler = new UpdateUserHandler(_userService.Object);
    }

    [Fact]
    public async Task Should_Update_User_Successfully()
    {
        // Arrange
        _userService
            .Setup(_ => _.UpdateAsync(It.IsAny<User>()))
            .ReturnsAsync(true);

        // Act
        var actual = await _handler.Handle(new UpdateUserCommand(), CancellationToken.None);

        // Assert
        actual.Should().BeTrue();
    }
}