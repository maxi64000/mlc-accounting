using FluentAssertions;
using MlcAccounting.Api.UserFeatures.CreateUser;
using MlcAccounting.Domain.UserAggregate.Builders;
using Xunit;

namespace MlcAccounting.Api.Tests.Unit.UserFeatures.CreateUser;

public class CreateUserMapperTests
{
    [Fact]
    public void Should_Map_To_Entity()
    {
        // Arrange
        var expected = new UserBuilder().WithUpdatedAt(null).Build();

        // Act
        var actual = new CreateUserCommand
        {
            Name = expected.Name,
            Password = expected.Password
        }.ToEntity();

        // Assert
        actual.Should().BeEquivalentTo(expected, _ => _.Excluding(_ => _.Id).Excluding(_ => _.CreatedAt));
    }
}