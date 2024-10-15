using Core.Application.Dtos;

namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimListItemDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public GetListUserOperationClaimListItemDto()
    {

    }

    public GetListUserOperationClaimListItemDto(int id, int userId, int operationClaimId)
    {
        Id = id;
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}