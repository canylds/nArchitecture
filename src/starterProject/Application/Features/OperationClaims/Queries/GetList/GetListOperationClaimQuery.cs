using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQuery : IRequest<IList<GetListOperationClaimListItemDto>>,
    ISecuredRequest
{
    public string[] Roles => [Admin, Read];

    public class GetListOperationClaimQueryHandler
        : IRequestHandler<GetListOperationClaimQuery, IList<GetListOperationClaimListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public GetListOperationClaimQueryHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository)
        {
            _mapper = mapper;
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task<IList<GetListOperationClaimListItemDto>> Handle(GetListOperationClaimQuery request,
            CancellationToken cancellationToken)
        {
            IList<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync(enableTracking: false,
                cancellationToken: cancellationToken);

            IList<GetListOperationClaimListItemDto> response =
                _mapper.Map<IList<GetListOperationClaimListItemDto>>(operationClaims);

            return response;
        }
    }
}
