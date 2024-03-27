using BirthdayChecker.Application.Services.Repositories;
using BirthdayChecker.Domain.Entities;
using BirthdayChecker.Persistence.Contexts;
using Core.Persistence.Repositories;

namespace BirthdayChecker.Persistence.Repositories;

public class UserRepository : EfRepositoryBase<User, int, BaseDbContext>, IUserRepository
{
    public UserRepository(BaseDbContext context) : base(context)
    {
    }
}
