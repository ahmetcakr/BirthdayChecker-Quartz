using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shared.Repositories.Abstract;

public interface IGenericContextRepository<T, TDbContext>
    where T : class
    where TDbContext : DbContext
{
    IQueryable<T> GetAll();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    Task<T> Save(T entity);
    Task<T> Update(T entity);
    Task Delete(T entity);
}
