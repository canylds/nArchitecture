using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Commands.UpdateFromAuth;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Application.Features.Users.Queries.GetPagedList;
using core.Application.Responses;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand)
    {
        CreatedUserResponse result = await Mediator.Send(createUserCommand);

        return Created(uri: string.Empty, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
    {
        UpdatedUserResponse result = await Mediator.Send(updateUserCommand);

        return Ok(result);
    }

    [HttpPut("FromAuth")]
    public async Task<IActionResult> UpdateFromAuth([FromBody] UpdateUserFromAuthCommand updateUserFromAuthCommand)
    {
        updateUserFromAuthCommand.Id = GetUserIdFromRequest();

        UpdatedUserFromAuthResponse result = await Mediator.Send(updateUserFromAuthCommand);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeleteUserCommand deleteUserCommand = new(id);

        DeletedUserResponse result = await Mediator.Send(deleteUserCommand);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdUserQuery getByIdUserQuery = new(id);

        GetByIdUserResponse result = await Mediator.Send(getByIdUserQuery);

        return Ok(result);
    }

    [HttpGet("GetFromAuth")]
    public async Task<IActionResult> GetFromAuth()
    {
        GetByIdUserQuery getByIdUserQuery = new(id: GetUserIdFromRequest());

        GetByIdUserResponse result = await Mediator.Send(getByIdUserQuery);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        GetListUserQuery getListUserQuery = new();

        IList<GetListUserListItemDto> result = await Mediator.Send(getListUserQuery);

        return Ok(result);
    }

    [HttpGet("GetPaged")]
    public async Task<IActionResult> GetPagedList([FromQuery] PageRequest pageRequest)
    {
        GetPagedListUserQuery getPagedListUserQuery = new(pageRequest);

        GetPagedListResponse<GetPagedListUserListItemDto> result = await Mediator.Send(getPagedListUserQuery);

        return Ok(result);
    }
}
