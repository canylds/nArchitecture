using Core.Application.Responses;

namespace Application.Features.OperationClaims.Commands.Delete;

public class DeletedOperationClaimResponse : IResponse
{
    public int Id { get; set; }

    public DeletedOperationClaimResponse()
    {

    }

    public DeletedOperationClaimResponse(int id)
    {
        Id = id;
    }
}