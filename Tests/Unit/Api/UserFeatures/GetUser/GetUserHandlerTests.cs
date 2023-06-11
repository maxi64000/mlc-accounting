using FluentAssertions;
using MlcAccounting.Api.UserFeatures.GetUser;
using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Builders;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MlcAccounting.Api.Tests.Unit.UserFeatures.GetUser;

public class GetUserHandlerTests
{
    private readonly Mock<IUserService> _userService;

    private readonly GetUserHandler _handler;

    public GetUserHandlerTests()
    {
        _userService = new Mock<IUserService>();

        _handler = new GetUserHandler(_userService.Object);
    }

    [Fact]
    public async Task Get_User_Successfully()
    {
        // Arrange
        var expected = new UserBuilder().Build();

        _userService
            .Setup(_ => _.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _handler.Handle(new GetUserQuery
        {
            Id = expected.Id
        }, CancellationToken.None);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}