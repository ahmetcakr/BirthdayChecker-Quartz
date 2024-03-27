using Microsoft.EntityFrameworkCore;
using Shared.Repositories.Abstract;
using Shared.Services.Abstract;
using System.Linq.Expressions;

namespace Shared.Services.Concrete;

public class GenericContextService<T, TDbContext> : IGenericContextService<T, TDbContext> where T : class where TDbContext : DbContext
{
    private IGenericContextRepository<T, TDbContext> _repository;

    public GenericContextService(IGenericContextRepository<T, TDbContext> repository)
    {
        _repository = repository;
    }

    public IEnumerable<T> GetAll()
    {
        return _repository.GetAll();
    }

    public IEnumerable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _repository.Where(expression).AsEnumerable();
    }
}
