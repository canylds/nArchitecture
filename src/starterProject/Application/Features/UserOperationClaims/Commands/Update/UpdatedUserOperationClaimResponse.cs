using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdatedUserOperationClaimResponse : IResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public UpdatedUserOperationClaimResponse()
    {

    }

    public UpdatedUserOperationClaimResponse(int id, int userId, int operationClaimId)
    {
        Id = id;
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}