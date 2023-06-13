using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Api.UserFeatures.CreateUser;
using MlcAccounting.Domain.UserAggregate.Builders;
using MlcAccounting.Domain.UserAggregate.Entities;
using MlcAccounting.Infrastructure.UserRepository;
using MlcAccounting.Infrastructure.UserRepository.Dtos;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace MlcAccounting.Api.Tests.Integration.Steps;

[Binding]
internal class UserStepDefinitions : ICollectionFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    private readonly User _user = new UserBuilder().Build();

    private HttpResponseMessage? _response;

    public UserStepDefinitions(TestWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        UserInMemoryRepository.Data.Clear();
    }

    [Given(@"the user already exist")]
    public void GivenTheUserAlreadyExist()
    {
        UserInMemoryRepository.Data.Add(new UserDto(_user));
    }

    [When(@"the GetUser request is called")]
    public async Task WhenTheGetUserRequestIsCalled()
    {
        _response = await _httpClient.GetAsync($"/users/{_user!.Id}");
    }

    [When(@"the CreateUser request is called")]
    public async Task WhenTheCreateUserRequestIsCalled()
    {
        _response = await _httpClient.PostAsync("/users", new StringContent(JsonConvert.SerializeObject(new CreateUserCommand
        {
            Name = _user!.Name,
            Password = _user!.Password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [When(@"the CreateUser request is called with ""([^""]*)"" name")]
    public async Task WhenTheCreateUserRequestIsCalledWithName(string name)
    {
        _response = await _httpClient.PostAsync("/users", new StringContent(JsonConvert.SerializeObject(new CreateUserCommand
        {
            Name = name,
            Password = _user!.Password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
    }

    [When(@"the CreateUser request is called with ""([^""]*)"" password")]
    public async Task WhenTheCreateUserRequestIsCalledWithPassword(string password)
    {
        _response = await _httpClient.PostAsync("/users", new StringContent(JsonConvert.SerializeObject(new CreateUserCommand
        {
            Name = _user!.Name,
            Password = password
        }), Encoding.UTF8, MediaTypeNames.Application.Json));
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

        actual.Should().BeEquivalentTo(_user);
    }

    [Then(@"the response header ""([^""]*)"" contains link to get the user")]
    public async Task ThenTheResponseHeaderContainsLinkToGetTheUser(string name)
    {
        var location = _response!.Headers.GetValues(name).FirstOrDefault();

        var actual = JsonConvert.DeserializeObject<User>(await (await _httpClient.GetAsync(location)).Content.ReadAsStringAsync());

        actual.Should().BeEquivalentTo(_user, _ => _.Excluding(_ => _.Id).Excluding(_ => _.CreatedAt).Excluding(_ => _.UpdatedAt));

        actual.UpdatedAt.Should().BeNull();
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
}