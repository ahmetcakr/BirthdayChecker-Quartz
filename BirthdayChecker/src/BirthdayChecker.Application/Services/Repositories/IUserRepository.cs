using BirthdayChecker.Domain.Entities;
using Core.Persistence.Repositories;

namespace BirthdayChecker.Application.Services.Repositories;

public interface IUserRepository : IAsyncRepository<User, int>, IRepository<User, int>
{
}
