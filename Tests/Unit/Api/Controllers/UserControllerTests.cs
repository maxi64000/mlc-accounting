using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Api.Controllers;
using MlcAccounting.Api.UserFeatures.CreateUser;
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
    public async Task GetUser_Successfully()
    {
        // Arrange
        var user = new UserBuilder().Build();

        var expected = new OkObjectResult(user);

        _mediator
            .Setup(_ => _.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var actual = (await _controller.GetUser(Guid.NewGuid())).Result as OkObjectResult;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetUser_When_Does_Not_Exist()
    {
        // Arrange
        var expected = new NotFoundObjectResult(new ProblemDetails
        {
            Title = "Not Found",
            Status = (int)HttpStatusCode.NotFound,
            Detail = "This user doesn't exist."
        });

        _mediator
            .Setup(_ => _.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as User);

        // Act
        var actual = (await _controller.GetUser(Guid.NewGuid())).Result as NotFoundObjectResult;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CreateUser_Successfully()
    {
        // Arrange
        var user = new UserBuilder().Build();

        var expected = new CreatedAtActionResult("GetUser", null, new { id = user.Id }, null);

        _mediator
            .Setup(_ => _.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user.Id);

        // Act
        var actual = await _controller.CreateUser(new CreateUserCommand()) as CreatedAtActionResult;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CreateUser_When_Already_Exist()
    {
        // Arrange
        var expected = new ConflictObjectResult(new ProblemDetails
        {
            Title = "Conflict",
            Status = (int)HttpStatusCode.Conflict,
            Detail = "This user already exist."
        });

        _mediator
            .Setup(_ => _.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as Guid?);

        // Act
        var actual = await _controller.CreateUser(new CreateUserCommand()) as ConflictObjectResult;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}