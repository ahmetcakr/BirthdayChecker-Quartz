using Application.Features.Users.Rules;
using AutoMapper;
using BirthdayChecker.Application.Services.Repositories;
using BirthdayChecker.Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<GetByIdUserResponse>
{
    public int Id { get; set; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public GetByIdUserQueryHandler(IMapper mapper, IUserRepository userRepository, UserBusinessRules userBusinessRules)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _userBusinessRules.UserShouldExistWhenSelected(user);

            GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);
            return response;
        }
    }
}