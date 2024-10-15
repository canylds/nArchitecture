using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimResponse : IResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public GetByIdUserOperationClaimResponse()
    {

    }

    public GetByIdUserOperationClaimResponse(int id, int userId, int operationClaimId)
    {
        Id = id;
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}