using Application.Features.Users.Rules;
using AutoMapper;
using BirthdayChecker.Application.Services.Repositories;
using BirthdayChecker.Domain.Entities;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserResponse>, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetUsers";

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository,
                                         UserBusinessRules userBusinessRules)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _userBusinessRules.UserShouldExistWhenSelected(user);
            user = _mapper.Map(request, user);

            await _userRepository.UpdateAsync(user!);

            UpdatedUserResponse response = _mapper.Map<UpdatedUserResponse>(user);
            return response;
        }
    }
}