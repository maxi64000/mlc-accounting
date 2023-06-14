using FluentAssertions;
using MlcAccounting.Domain.UserAggregate.Builders;
using MlcAccounting.Domain.UserAggregate.Entities;
using MlcAccounting.Infrastructure.UserRepository.Dtos;
using MlcAccounting.Infrastructure.UserRepository.Mappers;
using System.Collections.Generic;
using Xunit;

namespace MlcAccounting.Infrastructure.Tests.Unit.UserRepository.Mappers;

public class UserMapperTests
{
    [Fact]
    public void Should_Map_To_List_Of_Entities()
    {
        // Arrange
        var user = new UserBuilder().Build();

        var expected = new List<User> { user };

        // Act
        var actual = new List<UserDto> { new(user) }.ToEntity();

        // Assert
        actual.Should().BeEquivalentTo(expected, _ => _.Excluding(user => user.Id).Excluding(user => user.CreatedAt));
    }

    [Fact]
    public void Should_Map_To_Entity()
    {
        // Arrange
        var expected = new UserBuilder().Build();

        // Act
        var actual = new UserDto(expected).ToEntity();

        // Assert
        actual.Should().BeEquivalentTo(expected, _ => _.Excluding(user => user.Id).Excluding(user => user.CreatedAt));
    }
}