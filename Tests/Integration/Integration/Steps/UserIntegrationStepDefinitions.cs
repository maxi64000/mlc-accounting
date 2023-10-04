using FluentAssertions;
using MlcAccounting.Common.Integration.Entities;
using MlcAccounting.Common.Integration.Enums;
using MlcAccounting.Integration.Api.UserIntegrationFeatures.SubmitUserIntegration;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;
using MlcAccounting.Tests.Common.Integration.Builders;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace MlcAccounting.Integration.Tests.Integration.Steps;

[Binding]
internal class UserIntegrationStepDefinitions : ICollectionFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    private readonly IMongoCollection<UserIntegration> _collection;

    private HttpResponseMessage? _response;

    private UserIntegration _userIntegration;

    public UserIntegrationStepDefinitions(TestWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();

        _collection = new MongoClient(factory.UserIntegrationMongoRepositoryOptions.ConnectionString)
            .GetDatabase(factory.UserIntegrationMongoRepositoryOptions.Database)
            .GetCollection<UserIntegration>(factory.UserIntegrationMongoRepositoryOptions.Collection);

        _collection.DeleteMany(_ => true);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _collection.DeleteMany(_ => true);
    }

    [When(@"the submit user integration request is called")]
    public async Task WhenTheSubmitUserIntegrationRequestIsCalled()
    {
        _userIntegration = new UserIntegrationBuilder()
            .WithPackageId(null)
            .WithStatus(IntegrationStatus.Accepted)
            .Build();

        _response = await _httpClient.PostAsync("/user-integrations", new StringContent(JsonConvert.SerializeObject(new SubmitUserIntegrationCommand
        {
            Name = _userIntegration.Name,
            Password = _userIntegration.Password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [Then(@"the status code is (.*)")]
    public void ThenTheStatusCodeIs(int statusCode)
    {
        _response!.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"The user integration has been created")]
    public async Task ThenTheUserIntegrationHasBeenCreated()
    {
        _userIntegration.Id = Guid.Parse(_response.Headers.Location.Segments.Last());
        _userIntegration.Commentaries = new List<Commentary>
        {
            new(CommentaryType.Information, "The user has been created.")
        };

        var actual = await _collection.Find(_ => _.Id == _userIntegration.Id).SingleOrDefaultAsync();

        while (actual.Status == IntegrationStatus.InProgress)
        {
            await Task.Delay(250);

            actual = await _collection.Find(_ => _.Id == _userIntegration.Id).SingleOrDefaultAsync();
        }

        actual.Should().BeEquivalentTo(_userIntegration, _ => _.Using<DateTime>(_ => _.Subject.Should().BeCloseTo(_.Expectation, TimeSpan.FromSeconds(30))).WhenTypeIs<DateTime>());
    }
}