using static Application.Features.Users.Constants.UsersOperationClaims;
using Core.Application.Pipelines.Authorization;
using MediatR;
using AutoMapper;
using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<IList<GetListUserListItemDto>>, ISecuredRequest
{
    public string[] Roles => [Admin, Read];

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, IList<GetListUserListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetListUserQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IList<GetListUserListItemDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            IList<User> users = await _userRepository.GetListAsync(enableTracking: false,
                cancellationToken: cancellationToken);

            IList<GetListUserListItemDto> response = _mapper.Map<IList<GetListUserListItemDto>>(users);

            return response;
        }
    }
}
