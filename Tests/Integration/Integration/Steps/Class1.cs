//using FluentAssertions;
//using Flurl;
//using Flurl.Http;
//using MlcAccounting.Common.Integration.Entities;
//using MlcAccounting.Common.Integration.Enums;
//using MlcAccounting.Integration.Api.UserIntegrationFeatures.SubmitUserIntegration;
//using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;
//using MlcAccounting.Integration.Tests.Common.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TechTalk.SpecFlow;

//namespace MlcAccounting.Integration.Tests.Integration.Steps;

//[Binding]
//internal class UserIntegrationStepDefinitions
//{
//    private const string BaseUrl = "http://localhost:5082";

//    private IFlurlResponse _response;

//    private UserIntegration _userIntegration = new UserIntegrationBuilder().Build();

//    [When(@"the submit user integration request is called")]
//    public async Task WhenTheSubmitUserIntegrationRequestIsCalled()
//    {
//        _response = await BaseUrl
//            .AppendPathSegment("user-integrations")
//            .PostJsonAsync(new SubmitUserIntegrationCommand
//            {
//                Name = _userIntegration.Name,
//                Password = _userIntegration.Password
//            });
//    }

//    [Then(@"the status code is (.*)")]
//    public void ThenTheStatusCodeIs(int statusCode)
//    {
//        _response.StatusCode.Should().Be(statusCode);
//    }

//    [Then(@"The user integration has been created")]
//    public async Task ThenTheUserIntegrationHasBeenCreated()
//    {
//        _userIntegration = new UserIntegrationBuilder()
//            .WithId(Guid.Parse(_response.Headers.FirstOrDefault("Location").Split("/").Last()))
//            .WithPackageId(null)
//            .WithStatus(IntegrationStatus.Accepted)
//            .WithCommentaries(new List<Commentary>
//            {
//                new(CommentaryType.Information, "The user has been updated.")
//            }).Build();

//        var actual = await GetUserIntegration();

//        while (actual.Status == IntegrationStatus.InProgress)
//        {
//            await Task.Delay(100);

//            actual = await GetUserIntegration();
//        }

//        actual.Should().BeEquivalentTo(_userIntegration, _ => _.Using<DateTime>(_ => _.Subject.Should().BeCloseTo(_.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTime>());
//    }

//    private async Task<UserIntegration> GetUserIntegration() =>
//        await BaseUrl
//            .AppendPathSegment("user-integrations")
//            .AppendPathSegment(_userIntegration.Id)
//            .GetJsonAsync<UserIntegration>();
//}