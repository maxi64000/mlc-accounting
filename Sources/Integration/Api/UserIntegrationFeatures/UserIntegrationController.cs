using MediatR;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Integration.Api.UserIntegrationFeatures.GetAllUserIntegrations;
using MlcAccounting.Integration.Api.UserIntegrationFeatures.GetUserIntegration;
using MlcAccounting.Integration.Api.UserIntegrationFeatures.SubmitUserIntegration;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;
using System.Net;

namespace MlcAccounting.Integration.Api.UserIntegrationFeatures;

[ApiController]
[Route("user-integrations")]
public class UserIntegrationController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserIntegrationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetAllUserIntegrations([FromQuery] GetAllUserIntegrationsQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserIntegration>> GetUserIntegration([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetUserIntegrationQuery { Id = id });

        return result != null ? Ok(result) : NotFound(new ProblemDetails
        {
            Title = "Not Found",
            Status = (int)HttpStatusCode.NotFound,
            Detail = "This user integration doesn't exist."
        });
    }

    [HttpPost("")]
    public async Task<ActionResult> SubmitUserIntegration([FromBody] SubmitUserIntegrationCommand command) =>
        CreatedAtAction(nameof(GetUserIntegration), new { Id = await _mediator.Send(command) }, null);
}