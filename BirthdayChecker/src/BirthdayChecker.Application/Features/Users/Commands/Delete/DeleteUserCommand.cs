using Application.Features.Users.Rules;
using AutoMapper;
using BirthdayChecker.Application.Services.Repositories;
using BirthdayChecker.Domain.Entities;
using Core.Application.Pipelines.Caching;
using MediatR;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<DeletedUserResponse>, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetUsers";

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public DeleteUserCommandHandler(IMapper mapper, IUserRepository userRepository,
                                         UserBusinessRules userBusinessRules)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<DeletedUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _userBusinessRules.UserShouldExistWhenSelected(user);

            await _userRepository.DeleteAsync(user!);

            DeletedUserResponse response = _mapper.Map<DeletedUserResponse>(user);
            return response;
        }
    }
}