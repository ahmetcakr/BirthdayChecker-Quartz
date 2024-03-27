using BirthdayChecker.Domain.Entities;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BirthdayChecker.Application.Services.Users;

public interface IUsersService
{
    Task<User?> GetAsync(
    Expression<Func<User, bool>> predicate,
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<User>?> GetListAsync(
    Expression<Func<User, bool>>? predicate = null,
    Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
    bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<User> DeleteAsync(User user, bool permanent = false);
}
