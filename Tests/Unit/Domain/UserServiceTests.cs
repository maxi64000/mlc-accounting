using FluentAssertions;
using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Domain.UserAggregate.Builders;
using Moq;
using System;
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
    public async Task Get_User_Successfully()
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
}