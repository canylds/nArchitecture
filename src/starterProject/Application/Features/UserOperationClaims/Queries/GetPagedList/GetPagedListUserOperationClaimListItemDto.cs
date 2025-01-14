using Core.Application.Dtos;

namespace Application.Features.UserOperationClaims.Queries.GetPagedList;

public class GetPagedListUserOperationClaimListItemDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public GetPagedListUserOperationClaimListItemDto()
    {

    }

    public GetPagedListUserOperationClaimListItemDto(int id, int userId, int operationClaimId)
    {
        Id = id;
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}