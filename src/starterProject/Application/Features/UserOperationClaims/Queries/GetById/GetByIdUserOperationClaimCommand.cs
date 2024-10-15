using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimQuery : IRequest<GetByIdUserOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public GetByIdUserOperationClaimQuery()
    {

    }

    public GetByIdUserOperationClaimQuery(int id)
    {
        Id = id;
    }

    public class GetByIdUserOperationClaimQueryHandler : IRequestHandler<GetByIdUserOperationClaimQuery, GetByIdUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public GetByIdUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository,
        IMapper mapper,
        UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<GetByIdUserOperationClaimResponse> Handle(GetByIdUserOperationClaimQuery request,
        CancellationToken cancellationToken)
        {
            UserOperationClaim? userOperationClaim =
            await _userOperationClaimRepository.GetAsync(predicate: uoc => uoc.Id.Equals(request.Id),
            enableTracking: false,
            cancellationToken: cancellationToken);

            await _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenSelected(userOperationClaim);

            GetByIdUserOperationClaimResponse userOperationClaimDto =
            _mapper.Map<GetByIdUserOperationClaimResponse>(userOperationClaim);

            return userOperationClaimDto;
        }
    }
}