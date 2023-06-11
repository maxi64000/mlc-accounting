using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Api.Controllers;
using MlcAccounting.Api.UserFeatures.GetUser;
using MlcAccounting.Domain.UserAggregate.Builders;
using MlcAccounting.Domain.UserAggregate.Entities;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MlcAccounting.Api.Tests.Unit.Controllers;

public class UserControllerTests
{
    private readonly Mock<IMediator> _mediator;

    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mediator = new Mock<IMediator>();

        _controller = new UserController(_mediator.Object);
    }

    [Fact]
    public async Task Get_User_Successfully()
    {
        // Arrange
        var user = new UserBuilder().Build();

        var expected = new OkObjectResult(user);

        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var actual = (await _controller.GetUser(Guid.NewGuid())).Result as OkObjectResult;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Get_User_When_Does_Not_Exist()
    {
        // Arrange
        var expected = new NotFoundObjectResult(new ProblemDetails
        {
            Title = "Not Found",
            Status = (int)HttpStatusCode.NotFound,
            Detail = "This user doesn't exist."
        });

        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as User);

        // Act
        var actual = (await _controller.GetUser(Guid.NewGuid())).Result as NotFoundObjectResult;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}