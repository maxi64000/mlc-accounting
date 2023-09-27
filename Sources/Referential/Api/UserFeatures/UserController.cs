using MediatR;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Referential.Api.UserFeatures.CreateUser;
using MlcAccounting.Referential.Api.UserFeatures.DeleteUser;
using MlcAccounting.Referential.Api.UserFeatures.GetAllUsers;
using MlcAccounting.Referential.Api.UserFeatures.GetUser;
using MlcAccounting.Referential.Api.UserFeatures.UpdateUser;
using MlcAccounting.Referential.Domain.UserAggregate.Entities;
using System.Net;

namespace MlcAccounting.Referential.Api.UserFeatures;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetAllUsers([FromQuery] GetAllUsersQuery query) =>
        Ok(await _mediator.Send(query));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUser([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetUserQuery { Id = id });

        return result != null ? Ok(result) : NotFound(new ProblemDetails
        {
            Title = "Not Found",
            Status = (int)HttpStatusCode.NotFound,
            Detail = "This user doesn't exist."
        });
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var id = await _mediator.Send(command);

        if (id == null)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Conflict",
                Status = (int)HttpStatusCode.Conflict,
                Detail = "This user already exist."
            });
        }

        return CreatedAtAction(nameof(GetUser), new { id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;

        return await _mediator.Send(command)
            ? NoContent()
            : NotFound(new ProblemDetails
            {
                Title = "Not Found",
                Status = (int)HttpStatusCode.NotFound,
                Detail = "This user doesn't exist."
            });
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid id) =>
        await _mediator.Send(new DeleteUserCommand { Id = id })
            ? NoContent()
            : NotFound(new ProblemDetails
            {
                Title = "Not Found",
                Status = (int)HttpStatusCode.NotFound,
                Detail = "This user doesn't exist."
            });
}