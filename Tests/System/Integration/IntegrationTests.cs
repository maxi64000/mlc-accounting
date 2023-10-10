using FluentAssertions;
using Flurl;
using Flurl.Http;
using MlcAccounting.Common.Integration.Entities;
using MlcAccounting.Common.Integration.Enums;
using MlcAccounting.Integration.Api.UserIntegrationFeatures.SubmitUserIntegration;
using MlcAccounting.Tests.Common.Integration.Builders;
using MlcAccounting.Tests.Common.Referential.Builders;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using User = MlcAccounting.Referential.Domain.UserAggregate.Entities.User;
using UserIntegration = MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities.UserIntegration;

namespace MlcAccounting.Tests.System.Integration;

public class IntegrationTests : IClassFixture<IntegrationFixture>
{
    private readonly string _integrationBaseUrl = "http://localhost:5082";

    private readonly IMongoCollection<User> _userCollection;

    private readonly IMongoCollection<UserIntegration> _userIntegrationCollection;

    public IntegrationTests(IntegrationFixture fixture)
    {
        _userCollection = fixture.UserCollection;
        _userIntegrationCollection = fixture.UserIntegrationCollection;

        _userCollection.DeleteMany(_ => true);
        _userIntegrationCollection.DeleteMany(_ => true);
    }

    [Fact]
    public async Task Should_Submit_User_Integration_Successfully()
    {
        var user = new UserBuilder().Build();

        var userIntegration = new UserIntegrationBuilder()
            .WithPackageId(null)
            .WithStatus(IntegrationStatus.Accepted)
            .WithName(user.Name)
            .WithPassword(user.Password)
            .WithCommentaries(new List<Commentary>
            {
                new(CommentaryType.Information, "The user has been created.")
            }).Build();

        var response = await _integrationBaseUrl
            .AppendPathSegment("user-integrations")
            .PostJsonAsync(new SubmitUserIntegrationCommand
            {
                Name = user.Name,
                Password = user.Password
            });

        response.StatusCode.Should().Be(201);

        await Task.Delay(500);

        userIntegration.Id = Guid.Parse(response.Headers.FirstOrDefault("Location").Split("/").Last());

        var actual = await _userIntegrationCollection.Find(_ => _.Id == userIntegration.Id).SingleOrDefaultAsync();

        while (actual.Status == IntegrationStatus.InProgress)
        {
            await Task.Delay(250);

            actual = await _userIntegrationCollection.Find(_ => _.Id == userIntegration.Id).SingleOrDefaultAsync();
        }

        actual.Should().BeEquivalentTo(userIntegration, _ => _.Using<DateTime>(_ => _.Subject.Should().BeCloseTo(_.Expectation, TimeSpan.FromSeconds(30))).WhenTypeIs<DateTime>());

        (await _userCollection.Find(_ => _.Name == user.Name && _.Password == user.Password).SingleAsync()).Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Submit_User_Integration_When_User_Already_Exist()
    {
        var user = new UserBuilder().Build();

        await _userCollection.InsertOneAsync(user);

        var userIntegration = new UserIntegrationBuilder()
            .WithPackageId(null)
            .WithStatus(IntegrationStatus.Accepted)
            .WithName(user.Name)
            .WithPassword(user.Password)
            .WithCommentaries(new List<Commentary>
            {
                new(CommentaryType.Information, "The user has been updated.")
            }).Build();

        var response = await _integrationBaseUrl
            .AppendPathSegment("user-integrations")
            .PostJsonAsync(new SubmitUserIntegrationCommand
            {
                Name = user.Name,
                Password = user.Password
            });

        response.StatusCode.Should().Be(201);

        await Task.Delay(500);

        userIntegration.Id = Guid.Parse(response.Headers.FirstOrDefault("Location").Split("/").Last());

        var actual = await _userIntegrationCollection.Find(_ => _.Id == userIntegration.Id).SingleOrDefaultAsync();

        while (actual.Status == IntegrationStatus.InProgress)
        {
            await Task.Delay(250);

            actual = await _userIntegrationCollection.Find(_ => _.Id == userIntegration.Id).SingleOrDefaultAsync();
        }

        actual.Should().BeEquivalentTo(userIntegration, _ => _.Using<DateTime>(_ => _.Subject.Should().BeCloseTo(_.Expectation, TimeSpan.FromSeconds(30))).WhenTypeIs<DateTime>());

        (await _userCollection.Find(_ => _.Id == user.Id).SingleAsync()).Should().BeEquivalentTo(user, _ => _.Using<DateTime>(_ => _.Subject.Should().BeCloseTo(_.Expectation, TimeSpan.FromSeconds(30))).WhenTypeIs<DateTime>());
    }
}