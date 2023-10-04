using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Referential.Api.UserFeatures.CreateUser;
using MlcAccounting.Referential.Api.UserFeatures.UpdateUser;
using MlcAccounting.Referential.Domain.UserAggregate.Entities;
using MlcAccounting.Tests.Common.Referential.Builders;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace MlcAccounting.Referential.Tests.Integration.Steps;

[Binding]
internal class UserStepDefinitions : ICollectionFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    private readonly IMongoCollection<User> _collection;

    private HttpResponseMessage? _response;

    private readonly User _user;

    public UserStepDefinitions(TestWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();

        _collection = new MongoClient(factory.UserMongoRepositoryOptions.ConnectionString)
            .GetDatabase(factory.UserMongoRepositoryOptions.Database)
            .GetCollection<User>(factory.UserMongoRepositoryOptions.Collection);

        _user = new UserBuilder().Build();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _collection.DeleteMany(_ => true);
    }

    [Given(@"the user already exist")]
    public void GivenTheUserAlreadyExist()
    {
        _collection.InsertOne(_user);
    }

    [When(@"the get user request is called")]
    public async Task WhenTheGetUserRequestIsCalled()
    {
        _response = await _httpClient.GetAsync($"/users/{_user.Id}");
    }

    [When(@"the create user request is called")]
    public async Task WhenTheCreateUserRequestIsCalled()
    {
        _user.UpdatedAt = null;

        _response = await _httpClient.PostAsync("/users", new StringContent(JsonConvert.SerializeObject(new CreateUserCommand
        {
            Name = _user.Name,
            Password = _user.Password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [When(@"the create user request is called with ""([^""]*)"" name")]
    public async Task WhenTheCreateUserRequestIsCalledWithName(string name)
    {
        _response = await _httpClient.PostAsync("/users", new StringContent(JsonConvert.SerializeObject(new CreateUserCommand
        {
            Name = name,
            Password = _user.Password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [When(@"the create user request is called with ""([^""]*)"" password")]
    public async Task WhenTheCreateUserRequestIsCalledWithPassword(string password)
    {
        _response = await _httpClient.PostAsync("/users", new StringContent(JsonConvert.SerializeObject(new CreateUserCommand
        {
            Name = _user.Name,
            Password = password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [When(@"the update user request is called")]
    public async Task WhenTheUpdateUserRequestIsCalled()
    {
        _response = await _httpClient.PutAsync($"/users/{_user.Id}", new StringContent(JsonConvert.SerializeObject(new UpdateUserCommand
        {
            Name = _user.Name,
            Password = _user.Password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [When(@"the update user request is called with ""([^""]*)"" name")]
    public async Task WhenTheUpdateUserRequestIsCalledWithName(string name)
    {
        _response = await _httpClient.PutAsync($"/users/{_user.Id}", new StringContent(JsonConvert.SerializeObject(new UpdateUserCommand
        {
            Name = name,
            Password = _user.Password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [When(@"the update user request is called with ""([^""]*)"" password")]
    public async Task WhenTheUpdateUserRequestIsCalledWithPassword(string password)
    {
        _response = await _httpClient.PutAsync($"/users/{_user.Id}", new StringContent(JsonConvert.SerializeObject(new UpdateUserCommand
        {
            Name = _user.Name,
            Password = password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [When(@"the delete user request is called")]
    public async Task WhenTheDeleteUserRequestIsCalled()
    {
        _response = await _httpClient.DeleteAsync($"/users/{_user.Id}");
    }

    [Then(@"the status code is (.*)")]
    public void ThenTheStatusCodeIs(int statusCode)
    {
        _response!.StatusCode.Should().Be((HttpStatusCode)statusCode);
    }

    [Then(@"the response is equal to the user")]
    public async Task ThenTheResponseIsEqualToTheUser()
    {
        var actual = JsonConvert.DeserializeObject<User>(await _response!.Content.ReadAsStringAsync());

        actual.Should().BeEquivalentTo(_user, _ => _.Using<DateTime>(_ => _.Subject.Should().BeCloseTo(_.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTime>());
    }

    [Then(@"the response detail is equal to ""([^""]*)""")]
    public async Task ThenTheResponseDetailIsEqualTo(string expected)
    {
        var actual = JsonConvert.DeserializeObject<ValidationProblemDetails>(await _response!.Content.ReadAsStringAsync());

        actual!.Detail.Should().Be(expected);
    }

    [Then(@"the response errors ""([^""]*)"" is equal to ""([^""]*)""")]
    public async Task ThenTheResponseErrorsIsEqualTo(string key, string expected)
    {
        var actual = JsonConvert.DeserializeObject<ValidationProblemDetails>(await _response!.Content.ReadAsStringAsync());

        actual!.Errors[key].Should().BeEquivalentTo(expected);
    }

    [Then(@"the user has been created")]
    public async Task ThenTheUserHasBeenCreated()
    {
        _user.Id = Guid.Parse(_response.Headers.Location.Segments.Last());

        var actual = await _collection.Find(_ => _.Id == _user.Id).SingleOrDefaultAsync();

        actual.Should().BeEquivalentTo(_user, _ => _.Using<DateTime>(_ => _.Subject.Should().BeCloseTo(_.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTime>());
    }

    [Then(@"the user has been updated")]
    public async Task ThenTheUserHasBeenUpdated()
    {
        var actual = await _collection.Find(_ => _.Id == _user.Id).SingleOrDefaultAsync();

        actual.Should().BeEquivalentTo(_user, _ => _.Using<DateTime>(_ => _.Subject.Should().BeCloseTo(_.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTime>());
    }
}