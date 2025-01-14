using Core.Application.Dtos;

namespace Application.Features.OperationClaims.Queries.GetPagedList;

public class GetPagedListOperationClaimListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public GetPagedListOperationClaimListItemDto()
    {
        Name = string.Empty;
    }

    public GetPagedListOperationClaimListItemDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}