using Application.Features.OperationClaims.Commands.Create;
using Application.Features.OperationClaims.Commands.Delete;
using Application.Features.OperationClaims.Commands.Update;
using Application.Features.OperationClaims.Queries.GetById;
using Application.Features.OperationClaims.Queries.GetList;
using Application.Features.OperationClaims.Queries.GetPagedList;
using AutoMapper;
using core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, CreatedOperationClaimResponse>().ReverseMap();

        CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, UpdatedOperationClaimResponse>().ReverseMap();

        CreateMap<OperationClaim, DeleteOperationClaimCommand>().ReverseMap();
        CreateMap<OperationClaim, DeletedOperationClaimResponse>().ReverseMap();

        CreateMap<OperationClaim, GetByIdOperationClaimResponse>().ReverseMap();

        CreateMap<OperationClaim, GetListOperationClaimListItemDto>().ReverseMap();

        CreateMap<OperationClaim, GetPagedListOperationClaimListItemDto>().ReverseMap();
        CreateMap<IPaginate<OperationClaim>, GetPagedListResponse<GetPagedListOperationClaimListItemDto>>().ReverseMap();
    }
}