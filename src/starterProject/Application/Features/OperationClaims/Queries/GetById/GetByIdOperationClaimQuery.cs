using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimQuery : IRequest<GetByIdOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public GetByIdOperationClaimQuery()
    {

    }

    public GetByIdOperationClaimQuery(int id)
    {
        Id = id;
    }

    public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, GetByIdOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public GetByIdOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository,
        IMapper mapper,
        OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<GetByIdOperationClaimResponse> Handle(GetByIdOperationClaimQuery request,
        CancellationToken cancellationToken)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(predicate: oc => oc.Id == request.Id,
            cancellationToken: cancellationToken,
            enableTracking: false);

            await _operationClaimBusinessRules.OperationClaimShouldExistWhenSelected(operationClaim);

            GetByIdOperationClaimResponse response = _mapper.Map<GetByIdOperationClaimResponse>(operationClaim);

            return response;
        }
    }
}