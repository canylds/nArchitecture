using Application.Services.Repositories;
using AutoMapper;
using core.Application.Responses;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Queries.GetPagedList;

public class GetPagedListUserOperationClaimQuery : IRequest<GetPagedListResponse<GetPagedListUserOperationClaimListItemDto>>,
    ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public GetPagedListUserOperationClaimQuery()
    {
        PageRequest = new PageRequest
        {
            PageIndex = 0,
            PageSize = 10
        };
    }

    public GetPagedListUserOperationClaimQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetPagedListUserOperationClaimQueryHandler 
        : IRequestHandler<GetPagedListUserOperationClaimQuery, GetPagedListResponse<GetPagedListUserOperationClaimListItemDto>>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;

        public GetPagedListUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetPagedListResponse<GetPagedListUserOperationClaimListItemDto>> Handle(GetPagedListUserOperationClaimQuery request,
            CancellationToken cancellationToken)
        {
            IPaginate<UserOperationClaim> userOperationClaims =await _userOperationClaimRepository.GetPagedListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false,
                cancellationToken: cancellationToken);

            GetPagedListResponse<GetPagedListUserOperationClaimListItemDto> mappedUserOperationClaimListModel =
                _mapper.Map<GetPagedListResponse<GetPagedListUserOperationClaimListItemDto>>(userOperationClaims);

            return mappedUserOperationClaimListModel;
        }
    }
}
