using Application.Services.Repositories;
using AutoMapper;
using core.Application.Responses;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Queries.GetPagedList;

public class GetPagedListOperationClaimQuery : IRequest<GetPagedListResponse<GetPagedListOperationClaimListItemDto>>,
    ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public GetPagedListOperationClaimQuery()
    {
        PageRequest = new PageRequest
        {
            PageIndex = 0,
            PageSize = 10
        };
    }

    public GetPagedListOperationClaimQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetPagedListOperationClaimQueryHandler
        : IRequestHandler<GetPagedListOperationClaimQuery, GetPagedListResponse<GetPagedListOperationClaimListItemDto>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetPagedListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetPagedListResponse<GetPagedListOperationClaimListItemDto>> Handle(GetPagedListOperationClaimQuery request,
            CancellationToken cancellationToken)
        {
            IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetPagedListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false,
                cancellationToken: cancellationToken);

            GetPagedListResponse<GetPagedListOperationClaimListItemDto> response =
                _mapper.Map<GetPagedListResponse<GetPagedListOperationClaimListItemDto>>(operationClaims);

            return response;
        }
    }
}