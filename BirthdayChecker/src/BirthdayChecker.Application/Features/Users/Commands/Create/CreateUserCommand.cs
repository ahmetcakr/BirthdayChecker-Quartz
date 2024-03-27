using Application.Features.Users.Rules;
using AutoMapper;
using BirthdayChecker.Application.Services.Repositories;
using BirthdayChecker.Domain.Entities;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreatedUserResponse>, ICacheRemoverRequest
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetUsers";

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository,
                                         UserBusinessRules userBusinessRules)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);

            await _userRepository.AddAsync(user);

            CreatedUserResponse response = _mapper.Map<CreatedUserResponse>(user);
            return response;
        }
    }
}