using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Commands.Update;
using Application.Features.UserOperationClaims.Queries.GetById;
using Application.Features.UserOperationClaims.Queries.GetList;
using Application.Features.UserOperationClaims.Queries.GetPagedList;
using AutoMapper;
using core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.UserOperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, CreatedUserOperationClaimResponse>().ReverseMap();

        CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, UpdatedUserOperationClaimResponse>().ReverseMap();

        CreateMap<UserOperationClaim, DeleteUserOperationClaimCommand>().ReverseMap();
        CreateMap<UserOperationClaim, DeletedUserOperationClaimResponse>().ReverseMap();

        CreateMap<UserOperationClaim, GetByIdUserOperationClaimResponse>().ReverseMap();

        CreateMap<UserOperationClaim, GetListUserOperationClaimListItemDto>().ReverseMap();

        CreateMap<UserOperationClaim, GetPagedListUserOperationClaimListItemDto>().ReverseMap();
        CreateMap<IPaginate<UserOperationClaim>, GetPagedListResponse<GetPagedListUserOperationClaimListItemDto>>().ReverseMap();
    }
}