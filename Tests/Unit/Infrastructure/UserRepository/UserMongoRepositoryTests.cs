using FluentAssertions;
using MlcAccounting.Domain.UserAggregate.Builders;
using MlcAccounting.Infrastructure.UserRepository;
using MlcAccounting.Infrastructure.UserRepository.Dtos;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MlcAccounting.Infrastructure.Tests.Unit.UserRepository;

public class UserMongoRepositoryTests
{
    private readonly Mock<IMongoCollection<UserDto>> _collection;

    private readonly UserMongoRepository _repository;

    public UserMongoRepositoryTests()
    {
        _collection = new Mock<IMongoCollection<UserDto>>();

        _repository = new UserMongoRepository(_collection.Object);
    }

    [Fact]
    public async Task GetAllAsync_By_Name_Success()
    {
        // Arrange
        var expected = new List<UserDto> { new(new UserBuilder().Build()) };

        var result = new Mock<IAsyncCursor<UserDto>>();

        result
            .Setup(_ => _.Current)
            .Returns(expected);

        result
            .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true))
            .Returns(Task.FromResult(false));

        _collection
            .Setup(_ => _.FindAsync(It.IsAny<FilterDefinition<UserDto>>(), It.IsAny<FindOptions<UserDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result.Object);

        // Act
        var actual = await _repository.GetAllAsync(string.Empty);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetAsync_Successfully()
    {
        // Arrange
        var expected = new UserDto(new UserBuilder().Build());

        var result = new Mock<IAsyncCursor<UserDto>>();

        result
            .Setup(_ => _.Current)
            .Returns(new List<UserDto> { expected });

        result
            .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true))
            .Returns(Task.FromResult(false));

        _collection
            .Setup(_ => _.FindAsync(It.IsAny<FilterDefinition<UserDto>>(), It.IsAny<FindOptions<UserDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result.Object);

        // Act
        var actual = await _repository.GetAsync(Guid.Empty);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CreateAsync_Successfully()
    {
        // Act
        await _repository.CreateAsync(new UserBuilder().Build());

        // Assert
        _collection.Verify(_ => _.InsertOneAsync(It.IsAny<UserDto>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_Successfully()
    {
        // Act
        await _repository.UpdateAsync(new UserBuilder().Build());

        // Assert
        _collection.Verify(_ => _.ReplaceOneAsync(It.IsAny<FilterDefinition<UserDto>>(), It.IsAny<UserDto>(), It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Successfully()
    {
        // Act
        await _repository.DeleteAsync(Guid.NewGuid());

        // Assert
        _collection.Verify(_ => _.DeleteOneAsync(It.IsAny<FilterDefinition<UserDto>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}