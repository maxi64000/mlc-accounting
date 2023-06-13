using FluentAssertions;
using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Domain.UserAggregate.Builders;
using MlcAccounting.Domain.UserAggregate.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MlcAccounting.Domain.Tests.Unit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repository;

    private readonly UserService _service;

    public UserServiceTests()
    {
        _repository = new Mock<IUserRepository>();

        _service = new UserService(_repository.Object);
    }

    [Fact]
    public async Task GetAsync_Successfully()
    {
        // Arrange
        var expected = new UserBuilder().Build();

        _repository
            .Setup(_ => _.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _service.GetAsync(expected.Id);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CreateAsync_Successfully()
    {
        // Arrange
        var expected = new UserBuilder().Build();

        _repository
            .Setup(_ => _.GetAllAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<User>());

        // Act
        var actual = await _service.CreateAsync(expected);

        // Assert
        actual.Should().Be(expected.Id);

        _repository.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_When_Already_Exist()
    {
        // Arrange
        var user = new UserBuilder().Build();

        _repository
            .Setup(_ => _.GetAllAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<User> { new UserBuilder().Build() });

        // Act
        var actual = await _service.CreateAsync(user);

        // Assert
        actual.Should().BeNull();

        _repository.Verify(_ => _.CreateAsync(It.IsAny<User>()), Times.Never);
    }
}