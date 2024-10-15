using Application.Services.Repositories;
using AutoMapper;
using core.Application.Responses;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQuery : IRequest<GetListResponse<GetListOperationClaimListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public GetListOperationClaimQuery()
    {
        PageRequest = new PageRequest
        {
            PageIndex = 0,
            PageSize = 10
        };
    }

    public GetListOperationClaimQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, GetListResponse<GetListOperationClaimListItemDto>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListOperationClaimListItemDto>> Handle(GetListOperationClaimQuery request,
        CancellationToken cancellationToken)
        {
            IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync(index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken);

            GetListResponse<GetListOperationClaimListItemDto> response =
            _mapper.Map<GetListResponse<GetListOperationClaimListItemDto>>(operationClaims);

            return response;
        }
    }
}