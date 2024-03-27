using BirthdayChecker.Application.Services.Repositories;
using BirthdayChecker.Domain.Entities;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BirthdayChecker.Application.Services.Users;

public class UsersManager : IUsersService
{
    private readonly IUserRepository _userRepository;

    public UsersManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> AddAsync(User user)
    {
        User addedUser = await _userRepository.AddAsync(user);

        return addedUser;
    }

    public async Task<User> DeleteAsync(User user, bool permanent = false)
    {
        User deletedUser = await _userRepository.DeleteAsync(user);

        return deletedUser;
    }

    public async Task<User?> GetAsync(
        Expression<Func<User, bool>> predicate, 
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null, 
        bool withDeleted = false, 
        bool enableTracking = true, 
        CancellationToken cancellationToken = default)
    {
        User? user = await _userRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return user;
    }

    public async Task<IPaginate<User>?> GetListAsync(
        Expression<Func<User, bool>>? predicate = null, 
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, 
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null, 
        int index = 0, 
        int size = 10, 
        bool withDeleted = false, 
        bool enableTracking = true, 
        CancellationToken cancellationToken = default)
    {
        IPaginate<User> userList = await _userRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userList;
    }

    public async Task<User> UpdateAsync(User user)
    {
        User updatedUser = await _userRepository.UpdateAsync(user);

        return updatedUser;
    }
}
