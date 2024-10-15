using static Application.Features.Users.Constants.UsersOperationClaims;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Application.Services.Repositories;
using AutoMapper;
using Application.Features.Users.Rules;
using Domain.Entities;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<GetByIdUserResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public GetByIdUserQuery()
    {

    }

    public GetByIdUserQuery(int id)
    {
        Id = id;
    }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(predicate: u => u.Id.Equals(request.Id), enableTracking: false,
            cancellationToken: cancellationToken);

            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);

            return response;
        }
    }
}