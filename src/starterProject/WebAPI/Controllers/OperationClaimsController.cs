using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Queries.GetById;
using Application.Features.OperationClaims.Queries.GetList;
using Application.Features.OperationClaims.Queries.GetPagedList;
using core.Application.Responses;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationClaimsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
    {
        CreatedOperationClaimResponse result = await Mediator.Send(createOperationClaimCommand);

        return Created(uri: string.Empty, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
    {
        UpdatedOperationClaimResponse result = await Mediator.Send(updateOperationClaimCommand);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeleteOperationClaimCommand deleteOperationClaimCommand = new(id);

        DeletedOperationClaimResponse result = await Mediator.Send(deleteOperationClaimCommand);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdOperationClaimQuery getByIdOperationClaimQuery = new(id);

        GetByIdOperationClaimResponse result = await Mediator.Send(getByIdOperationClaimQuery);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        GetListOperationClaimQuery getListOperationClaimQuery = new();

        IList<GetListOperationClaimListItemDto> result = await Mediator.Send(getListOperationClaimQuery);

        return Ok(result);
    }

    [HttpGet("Paged")]
    public async Task<IActionResult> GetPagedList([FromQuery] PageRequest pageRequest)
    {
        GetPagedListOperationClaimQuery getPagedListOperationClaimQuery = new(pageRequest);

        GetPagedListResponse<GetPagedListOperationClaimListItemDto> result =
            await Mediator.Send(getPagedListOperationClaimQuery);

        return Ok(result);
    }
}
