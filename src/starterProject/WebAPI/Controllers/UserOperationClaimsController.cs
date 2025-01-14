using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Commands.Update;
using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using Application.Features.UserOperationClaims.Queries.GetPagedList;
using core.Application.Responses;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
    {
        CreatedUserOperationClaimResponse result = await Mediator.Send(createUserOperationClaimCommand);

        return Created(uri: string.Empty, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
    {
        UpdatedUserOperationClaimResponse result = await Mediator.Send(updateUserOperationClaimCommand);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeleteUserOperationClaimCommand deleteUserOperationClaimCommand = new(id);

        DeletedUserOperationClaimResponse result = await Mediator.Send(deleteUserOperationClaimCommand);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdUserOperationClaimQuery getByIdUserOperationClaimQuery = new(id);

        GetByIdUserOperationClaimResponse result = await Mediator.Send(getByIdUserOperationClaimQuery);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        GetListUserOperationClaimQuery getListUserOperationClaimQuery = new();

        IList<GetListUserOperationClaimListItemDto> result = await Mediator.Send(getListUserOperationClaimQuery);

        return Ok(result);
    }

    [HttpGet("GetPaged")]
    public async Task<IActionResult> GetPagedList([FromQuery] PageRequest pageRequest)
    {
        GetPagedListUserOperationClaimQuery getPagedListUserOperationClaimQuery = new(pageRequest);

        GetPagedListResponse<GetPagedListUserOperationClaimListItemDto> result =
            await Mediator.Send(getPagedListUserOperationClaimQuery);

        return Ok(result);
    }
}
