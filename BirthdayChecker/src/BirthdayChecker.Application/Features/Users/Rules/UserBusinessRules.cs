using Application.Features.Users.Constants;
using BirthdayChecker.Application.Services.Repositories;
using BirthdayChecker.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;


namespace Application.Features.Users.Rules;

public class UserBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;

    public UserBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task UserShouldExistWhenSelected(User? user)
    {
        if (user == null)
            throw new BusinessException(UsersBusinessMessages.UserNotExists);
        return Task.CompletedTask;
    }

    public async Task UserIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await UserShouldExistWhenSelected(user);
    }
}