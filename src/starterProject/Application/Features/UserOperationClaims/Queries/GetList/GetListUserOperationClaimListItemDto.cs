using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimListItemDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public GetListUserOperationClaimListItemDto()
    {

    }

    public GetListUserOperationClaimListItemDto(int id, int userId, int operationClaimId)
    {
        Id = id;
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}

public class GetListUserOperationClaimQuery : IRequest<IList<GetListUserOperationClaimListItemDto>>,
    ISecuredRequest
{
    public string[] Roles => [Admin, Read];

    public class GetListUserOperationClaimQueryHandler
        : IRequestHandler<GetListUserOperationClaimQuery, IList<GetListUserOperationClaimListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public GetListUserOperationClaimQueryHandler(IMapper mapper,
            IUserOperationClaimRepository userOperationClaimRepository)
        {
            _mapper = mapper;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<IList<GetListUserOperationClaimListItemDto>> Handle(GetListUserOperationClaimQuery request,
            CancellationToken cancellationToken)
        {
            IList<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(
                enableTracking: false,
                cancellationToken: cancellationToken);

            IList<GetListUserOperationClaimListItemDto> response =
                _mapper.Map<IList<GetListUserOperationClaimListItemDto>>(userOperationClaims);

            return response;
        }
    }
}