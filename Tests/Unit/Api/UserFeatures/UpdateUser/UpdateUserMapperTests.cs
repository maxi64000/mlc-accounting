using FluentAssertions;
using MlcAccounting.Api.UserFeatures.UpdateUser;
using MlcAccounting.Domain.UserAggregate.Builders;
using Xunit;

namespace MlcAccounting.Api.Tests.Unit.UserFeatures.UpdateUser;

public class UpdateUserMapperTests
{
    [Fact]
    public void Should_Map_To_Entity()
    {
        // Arrange
        var expected = new UserBuilder().WithUpdatedAt(null).Build();

        // Act
        var actual = new UpdateUserCommand
        {
            Id = expected.Id,
            Name = expected.Name,
            Password = expected.Password
        }.ToEntity();

        // Assert
        actual.Should().BeEquivalentTo(expected, _ => _.Excluding(user => user.CreatedAt));
    }
}