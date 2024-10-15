using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeletedUserOperationClaimResponse : IResponse
{
    public int Id { get; set; }

    public DeletedUserOperationClaimResponse()
    {

    }

    public DeletedUserOperationClaimResponse(int id)
    {
        Id = id;
    }
}