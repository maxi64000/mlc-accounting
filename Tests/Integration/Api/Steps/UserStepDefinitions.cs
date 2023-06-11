using FluentAssertions;
using MlcAccounting.Domain.UserAggregate.Builders;
using MlcAccounting.Domain.UserAggregate.Entities;
using MlcAccounting.Infrastructure.UserRepositories;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace MlcAccounting.Api.Tests.Integration.Steps;

[Binding]
internal class UserStepDefinitions : ICollectionFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    private HttpResponseMessage? _response;

    private User? _user;

    public UserStepDefinitions(TestWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Given(@"the user already exist")]
    public void GivenTheUserAlreadyExist()
    {
        _user = new UserBuilder().Build();

        UserInMemoryRepository.Data.Add(_user);
    }

    [Given(@"the user does not exist")]
    public void GivenTheUserDoesNotExist()
    {
        _user = new UserBuilder().Build();
    }

    [When(@"the GetUser request is called")]
    public async Task WhenTheGetUserRequestIsCalled()
    {
        _response = await _httpClient.GetAsync($"/users/{_user!.Id}");
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

}