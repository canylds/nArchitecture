using static Application.Features.Users.Constants.UsersOperationClaims;
using Application.Services.Repositories;
using AutoMapper;
using core.Application.Responses;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetPagedList;

public class GetPagedListUserQuery : IRequest<GetPagedListResponse<GetPagedListUserListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public GetPagedListUserQuery()
    {
        PageRequest = new PageRequest
        {
            PageIndex = 0,
            PageSize = 10
        };
    }

    public GetPagedListUserQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetPagedListUserQueryHandler
        : IRequestHandler<GetPagedListUserQuery, GetPagedListResponse<GetPagedListUserListItemDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetPagedListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetPagedListResponse<GetPagedListUserListItemDto>> Handle(GetPagedListUserQuery request,
            CancellationToken cancellationToken)
        {
            IPaginate<User> users = await _userRepository.GetPagedListAsync(index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                enableTracking: false,
                cancellationToken: cancellationToken);

            GetPagedListResponse<GetPagedListUserListItemDto> response =
                _mapper.Map<GetPagedListResponse<GetPagedListUserListItemDto>>(users);

            return response;
        }
    }
}