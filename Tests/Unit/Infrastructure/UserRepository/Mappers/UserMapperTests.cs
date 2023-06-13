using FluentAssertions;
using MlcAccounting.Domain.UserAggregate.Builders;
using MlcAccounting.Infrastructure.UserRepository.Dtos;
using MlcAccounting.Infrastructure.UserRepository.Mappers;
using Xunit;

namespace MlcAccounting.Infrastructure.Tests.Unit.UserRepository.Mappers;

public class UserMapperTests
{
    [Fact]
    public void Should_Map_To_Entity()
    {
        // Arrange
        var expected = new UserBuilder().WithUpdatedAt(null).Build();

        // Act
        var actual = new UserDto(expected).ToEntity();

        // Assert
        actual.Should().BeEquivalentTo(expected, _ => _.Excluding(_ => _.Id).Excluding(_ => _.CreatedAt));
    }
}